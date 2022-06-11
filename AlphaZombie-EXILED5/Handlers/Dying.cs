using Exiled.Events.EventArgs;

namespace AlphaZombie.Handlers
{
    internal class Dying
    {
        public void OnDying(DyingEventArgs ev)
        {
            //Destroys Alpha Zombie when they die
            if (ev.Target.IsAlphaZombie())
            {
                ev.Target.DestroyAlphaZombie();
                Functions.AlphaZombieDeathAnnouce(ev.Handler.CustomBase.Type, ev.Killer, ev.Target);
            }
        }
    }
}