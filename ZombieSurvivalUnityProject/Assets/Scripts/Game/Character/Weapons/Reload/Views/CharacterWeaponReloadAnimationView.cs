using System;
using UnityEngine;

namespace Game.Character.Weapons.Reload.Views
{
    public class CharacterWeaponReloadAnimationView : MonoBehaviour
    {
        public Action<ReloadAnimationEventId> OnTriggerReloadEvent { get; set; }
        
        // triggered at animation (!not redundant!)
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
