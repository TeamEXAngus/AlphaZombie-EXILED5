using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Enums;
using PlayerStatsSystem;

namespace AlphaZombie.Components
{
    internal class AZController : MonoBehaviour
    {
        private static Config Configs => AlphaZombie.Instance.Config;

        private Player AZPlayer;
        private AhpStat.AhpProcess HumeShieldProcess;
        internal float TimeSinceTakenDamage = 0;

        public void Awake()
        {
            AZPlayer = Player.Get(gameObject);

            HumeShieldProcess = AZPlayer.ReferenceHub.playerStats.GetModule<AhpStat>()
                .ServerAddProcess(Configs.AlphaZombieMaxHS, Configs.AlphaZombieMaxHS,
                decay: 0, efficacy: 1, sustain: 0, persistant: true);
        }

        public void Update()
        {
            TimeSinceTakenDamage += Time.deltaTime;

            if (TimeSinceTakenDamage > Configs.AlphaZombieHSRegenTime)
            {
                HumeShieldProcess.CurrentAmount += Configs.AlphaZombieHSRegentAmount * Time.deltaTime;
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
        }
    }
}