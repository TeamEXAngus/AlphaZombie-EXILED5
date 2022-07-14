using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;

namespace AlphaZombie
{
    public sealed class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled on this server.")]
        public bool IsEnabled { get; set; } = true;

        [Description("The scale that should be applied to the Alpha Zombie.")]
        public Dictionary<string, float> AlphaZombieScale { get; set; } = new Dictionary<string, float>
        {
            { "x", 1.2f },
            { "y", 1.1f },
            { "z", 1.2f }
        };

        [Description("The max health of the Alpha Zombie.")]
        public int AlphaZombieMaxHP { get; set; } = 2000;

        [Description("The max Hume Shield of the Alpha Zombie.")]
        public int AlphaZombieMaxHS { get; set; } = 1000;

        [Description("The number of seconds after taking damage before an Alpha Zombie starts regenerating Hume Shield.")]
        public int AlphaZombieHSRegenTime { get; set; } = 30;

        [Description("The amount of Hume Shield regenerated per second.")]
        public int AlphaZombieHSRegentAmount { get; set; } = 10;

        [Description("The minimum number of players in the server for an Alpha Zombie to spawn alongisde an SCP-049.")]
        public int MinPlayersForSpawn { get; set; } = 4;

        [Description("The percent chance that an Alpha Zombie will spawn INSTEAD OF another SCP.")]
        public int AlphaZombieSpawnChance { get; set; } = 20;

        [Description("The percent chance that an Alpha Zombie will spawn AS WELL AS SCP-049")]
        public int AlphaZombieSpawnAlongside049Chance { get; set; } = 40;

        [Description("How long in seconds should the game wait before changing the Alpha Zombie's size and HP. Set this to a higher value if scale or HP aren't working.")]
        public float SpawnDelay { get; set; } = 1.5f;

        [Description("The text that should be shown at the top of the screen when a player spawns as Alpha Zombie. Set to \"none\" to disable.")]
        public string BroadcastOnSpawn { get; set; } = "You are <color=#ff0000>SCP-049-2-Alpha</color>!\nYou move faster than regular zombies\nWhen you attack a player they become <color=#00ff00>poisoned</color>";

        [Description("The text that should be shown at the top of the screen when a player is hit by Alpha Zombie. Set to \"none\" to disable.")]
        public string BroadcastWhenHit { get; set; } = "You have been <color=#00ff00>poisoned</color>!\nUse a <color=#ff0000>Medkit</color> or <color=#ff0000>SCP-500</color> to cure yourself!";

        [Description("The amount of time that text broadcasts from this plugin should remain on screen.")]
        public ushort BroadcastDuration { get; set; } = 5;

        [Description("Whether or not CASSIE should make an announcement when Alpha Zombie dies.")]
        public bool DeathAnnounce { get; set; } = true;

        [Description("How much damage the Alpha Zombie's attack should do.")]
        public int AlphaZombieAttackDamage { get; set; } = 40;

        [Description("What status effects the Alpha Zombie gives players who it hits.")]
        public List<EffectType> AlphaZombieInflict { get; set; } = new List<EffectType> { EffectType.Poisoned };

        [Description("Whether or not using a medkit cures the Poisoned status effect (unless the player is under the effect of SCP-207 and SCP-1853).")]
        public bool MedkitCurePoison { get; set; } = true;

        [Description("Should debug messages be shown in the server console?")]
        public bool DebugMessages { get; set; } = true;
    }
}