using Exiled.API.Features;
using System;
using UnityEngine;
using PlayerHandler = Exiled.Events.Handlers.Player;
using ServerHandler = Exiled.Events.Handlers.Server;

namespace AlphaZombie
{
    public class AlphaZombie : Plugin<Config>
    {
        internal static AlphaZombie Instance;

        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 1);
        public override Version Version { get; } = new Version(1, 0, 0);

        public override string Name { get; } = "Alpha Zombie";
        public override string Author { get; } = "TeamEXAngus#5525";

        private Handlers.Dying dying;
        private Handlers.Hurting hurting;
        private Handlers.ChangingRole changingRole;
        private Handlers.RoundStarting starting;

        internal bool ShouldSAZ; // SAZ = Spawn Alpha Zombie
        internal bool ShouldSAZ_If049;

        public AlphaZombie()
        {
            Instance = this;
        }

        public override void OnEnabled()
        {
            dying = new Handlers.Dying();
            hurting = new Handlers.Hurting();
            changingRole = new Handlers.ChangingRole();
            starting = new Handlers.RoundStarting();

            PlayerHandler.Hurting += hurting.OnHurting;
            PlayerHandler.Dying += dying.OnDying;
            PlayerHandler.ChangingRole += changingRole.OnChangingRole;
            ServerHandler.RoundStarted += starting.OnRoundStarting;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerHandler.Hurting -= hurting.OnHurting;
            PlayerHandler.Dying -= dying.OnDying;
            PlayerHandler.ChangingRole -= changingRole.OnChangingRole;
            ServerHandler.RoundStarted -= starting.OnRoundStarting;

            dying = null;
            hurting = null;
            changingRole = null;
            starting = null;

            base.OnDisabled();
        }
    }

    public class Test : MonoBehaviour
    {
        private void Update()
        {
            Log.Debug("abcde");
        }
    }
}