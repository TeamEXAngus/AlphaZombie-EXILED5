using MEC;
using System.Linq;
using UnityEngine;
using Exiled.API.Enums;
using NorthwoodLib.Pools;
using Exiled.API.Features;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace AlphaZombie.Handlers
{
    internal class ChangingRole
    {
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            DestroyAZIfClassChanged(ev.Player);

            if (ev.Reason != SpawnReason.RoundStart)
                return;

            if (ev.NewRole.GetSide() == Side.Scp &&
                AlphaZombie.Instance.ShouldSAZ)
            {
                AlphaZombie.Instance.ShouldSAZ = false;
                ev.Player.SpawnAlphaZombie();
                return;
            }

            if (ev.NewRole == RoleType.Scp049 &&
                AlphaZombie.Instance.ShouldSAZ_If049)
            {
                AlphaZombie.Instance.ShouldSAZ_If049 = false;
                TrySpawnAZIf049Spawned();
                return;
            }
        }

        private void DestroyAZIfClassChanged(Player Player)
        {
            if (Player.IsAlphaZombie() && Player.Role != RoleType.Scp0492)
            {
                Player.DestroyAlphaZombie();
            }
        }

        private void TrySpawnAZIf049Spawned()
        {
            SpawningAZ(out bool SpawnSuccess);
            Log.Debug($"Failed to spawn Alpha Zombie.", !SpawnSuccess && AlphaZombie.Instance.Config.DebugMessages);
        }

        //Boolean return value states whether a player was spawned or not.
        private void SpawningAZ(out bool Success)
        {
            var PlayerList = ListPool<Player>.Shared.Rent(Player.List);

            bool EnoughPlayers = PlayerList.Count >= AlphaZombie.Instance.Config.MinPlayersForSpawn;

            if (!EnoughPlayers)
            {
                Success = false;
                return;
            }

            Player NewAlphaZombie = TryChoosePlayer(out bool SuccessfullyChosePlayer, PlayerList);

            if (!SuccessfullyChosePlayer)
            {
                Success = false;
                return;
            }

            //Players are spawned one-by-one, so CallDelayed() prevents players from being set to Alpha Zombie then back to a normal class
            Timing.CallDelayed(1f, () => NewAlphaZombie.SpawnAlphaZombie());

            Success = true;
            return;
        }

        private Player TryChoosePlayer(out bool Success, List<Player> playerList)
        {
            var ListOfPlayersNot049 = playerList.Where(Ply => Ply.Role != RoleType.Scp049).ToList();
            var ListCount = ListOfPlayersNot049.Count;

            if (ListCount == 0)
            {
                Log.Debug("Failed to spawn Alpha Zombie because no players exist which are not SCP-049", AlphaZombie.Instance.Config.DebugMessages);

                Success = false;
                return null;
            }

            Success = true;
            return ListOfPlayersNot049[Random.Range(0, ListCount + 1)];
        }
    }
}