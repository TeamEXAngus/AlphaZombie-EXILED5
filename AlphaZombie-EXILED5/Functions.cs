using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Extensions;
using AlphaZombie.Components;
using UnityEngine;
using System;

namespace AlphaZombie
{
    internal static class Functions
    {
        private static Config Configs => AlphaZombie.Instance.Config;

        //Turns a player into an Alpha Zombie
        public static void SpawnAlphaZombie(this Player player)
        {
            (player.Position, _) = RoleExtensions.GetRandomSpawnProperties(RoleType.Scp049); // GetRandomSpawnProperties returns Tuple<Vector3, float>
            player.SetRole(RoleType.Scp0492, lite: true);
            player.ClearInventory();

            if (Configs.BroadcastOnSpawn != "none")
                player.Broadcast(Configs.BroadcastDuration, Configs.BroadcastOnSpawn);

            if (!player.IsAlphaZombie())
                player.GameObject.AddComponent<AZController>();
        }

        //Stops a player from being an Alpha Zombie
        public static void DestroyAlphaZombie(this Player player)
        {
            player.GameObject.TryGetComponent<AZController>(out var Controller);
            UnityEngine.Object.Destroy(Controller);
        }

        //Announces the death of an Alpha Zombie through CASSIE
        public static void AlphaZombieDeathAnnouce(DamageType damageType, Player killer, Player target)
        {
            if (!Configs.DeathAnnounce)
                return;

            const string Name = "SCP 0 4 9 2 nato_a";

            if (killer is not null)
            {
                if (killer.Role.Team == Team.MTF)
                {
                    AnnounceUsingCassie($"{Name} contained succesfully, containmenut unit {UnitNameToCassieWords(killer.UnitName)}.");
                    return;
                }

                AnnounceUsingCassie($"{Name} succesfully terminated by {killer.Role}.");
                return;
            }

            switch (damageType)
            {
                case DamageType.Decontamination:
                    AnnounceUsingCassie($"{Name} lost in Decontamination Sequence.");
                    return;

                case DamageType.Tesla:
                    AnnounceUsingCassie($"{Name} succesfully terminated by Automatic Security System.");
                    return;

                case DamageType.Warhead:
                    AnnounceUsingCassie($"{Name} terminated by Alpha Warhead.");
                    return;

                default:
                    AnnounceUsingCassie($"{Name} terminated. Termination cause unspecified.");
                    return;
            }
        }

        //Sends a CASSIE announcement with the configured glitch chance
        public static void AnnounceUsingCassie(string message)
        {
            Cassie.GlitchyMessage(message, 0.2f, 0.2f);
        }

        //Turns Player.UnitName into a CASSIE-readable string
        public static string UnitNameToCassieWords(this string unit) => $"nato_{unit[0]} {unit.Substring(unit.Length - 2)}";

        public static string RoleTypeToCassieWords(this RoleType role)
        {
            return role switch
            {
                RoleType.Scientist => "Science Personnel",
                RoleType.ClassD => "Class D Personnel",
                RoleType.ChaosConscript => "Chaos Insurgency",
                RoleType.ChaosMarauder => "Chaos Insurgency",
                RoleType.ChaosRepressor => "Chaos Insurgency",
                RoleType.ChaosRifleman => "Chaos Insurgency",
                _ => "Unknown Personnel",
            };
        }

        public static bool IsAlphaZombie(this Player player, out AZController Controller)
        {
            return player.GameObject.TryGetComponent(out Controller);
        }

        public static bool IsAlphaZombie(this Player player)
        {
            return IsAlphaZombie(player, out _);
        }

        public static bool PercentChance(int chance) => chance >= UnityEngine.Random.Range(1, 101);
    }
}