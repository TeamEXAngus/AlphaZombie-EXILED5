using CustomPlayerEffects;
using Exiled.Events.EventArgs;

namespace AlphaZombie.Handlers
{
    internal class UsedItem
    {
        private Config Configs => AlphaZombie.Instance.Config;

        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.Medkit || !Configs.MedkitCurePoison)
                return;

            if (ev.Player.GetEffectActive<Scp207>() &&
                ev.Player.GetEffectActive<Scp1853>())
                return;

            ev.Player.DisableEffect<Poisoned>();
        }
    }
}