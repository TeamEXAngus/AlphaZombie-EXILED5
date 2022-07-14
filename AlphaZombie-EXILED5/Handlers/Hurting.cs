using Exiled.API.Enums;
using Exiled.Events.EventArgs;

namespace AlphaZombie.Handlers
{
    internal class Hurting
    {
        public void OnHurting(HurtingEventArgs ev)
        {
            //Inflicts status effects and damage, and sends broadcast, when player is hit by Alpha Zombie
            if (ev.Attacker is not null && ev.Attacker.IsAlphaZombie())
            {
                HitByAlphaZombie(ev);
            }

            //Alpha Zombie Damage Handlers
            else if (ev.Target.IsAlphaZombie())
            {
                AlphaZombieDamageHandlers(ev);
            }
        }

        //Called when a player is hit by Alpha Zombie, sends broadcast and inflicts status effects
        public void HitByAlphaZombie(HurtingEventArgs ev)
        {
            if (AlphaZombie.Instance.Config.BroadcastWhenHit != "none")
                ev.Target.Broadcast(AlphaZombie.Instance.Config.BroadcastDuration, AlphaZombie.Instance.Config.BroadcastWhenHit);

            ev.Amount = AlphaZombie.Instance.Config.AlphaZombieAttackDamage;

            foreach (EffectType effect in AlphaZombie.Instance.Config.AlphaZombieInflict)
                ev.Target.EnableEffect(effect);
        }

        //Called when Alpha Zombie takes damage, prevents coke damage and modifies decont damage
        public void AlphaZombieDamageHandlers(HurtingEventArgs ev)
        {
            if (ev.Handler.CustomBase.Type == DamageType.Scp207)
            {
                ev.IsAllowed = false;
                return;
            }

            if (ev.Target.IsAlphaZombie(out var Controller))
            {
                Controller.TimeSinceTakenDamage = 0;
            }
        }
    }
}