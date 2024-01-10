using System;
using UnityEngine;

namespace Game.Character.Weapons.Reload.Views
{
    public class WeaponReloadAnimationView : MonoBehaviour
    {
        public Action<ReloadAnimationEventId> OnTriggerReloadEvent { get; set; }

        public void TriggerReloadEvent(ReloadAnimationEventId reloadAnimationEventId)
        {
            OnTriggerReloadEvent?.Invoke(reloadAnimationEventId);
        }
    }

    public enum ReloadAnimationEventId
    {
        DetachMagazine,
        DropMagazine,
        RefillMagazine,
        AttachMagazine,
    }
}
