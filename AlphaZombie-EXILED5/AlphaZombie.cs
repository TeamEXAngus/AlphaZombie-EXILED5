using Exiled.API.Features;
using System;
using PlayerHandler = Exiled.Events.Handlers.Player;

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
        private Handlers.Spawning spawning;


        public AlphaZombie()
        {
            Instance = this;
        }

        public override void OnEnabled()
        {
            dying = new Handlers.Dying();
            hurting = new Handlers.Hurting();
            spawning = new Handlers.Spawning();

            PlayerHandler.Hurting += hurting.OnHurting;
            PlayerHandler.Dying += dying.OnDying;
            PlayerHandler.Spawning += spawning.OnSpawning;

            Log.Info("Loaded Alpha Zombie plugin!");
        }

        public override void OnDisabled()
        {
            PlayerHandler.Hurting -= hurting.OnHurting;
            PlayerHandler.Dying -= dying.OnDying;
            PlayerHandler.Spawning -= spawning.OnSpawning;

            dying = null;
            hurting = null;
            spawning = null;

            Log.Info("Disabled Alpha Zombie plugin!");
        }
    }
}