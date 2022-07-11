using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;

namespace AlphaZombie.Handlers
{
    internal class RoundStarting
    {
        public void OnRoundStarting()
        {
            AlphaZombie.Instance.ShouldSAZ = false;
            AlphaZombie.Instance.ShouldSAZ_If049 = false;

            AlphaZombie.Instance.ShouldSAZ = Functions.PercentChance(AlphaZombie.Instance.Config.AlphaZombieSpawnChance);

            if (AlphaZombie.Instance.ShouldSAZ)
                return;

            AlphaZombie.Instance.ShouldSAZ_If049 = Functions.PercentChance(AlphaZombie.Instance.Config.AlphaZombieSpawnAlongside049Chance);
        }
    }
}
