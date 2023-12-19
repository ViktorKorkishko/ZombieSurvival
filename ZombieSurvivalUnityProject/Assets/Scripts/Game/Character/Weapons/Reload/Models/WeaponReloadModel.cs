using System;

namespace Game.Character.Weapons.Reload.Models
{
    public class WeaponReloadModel
    {
        public Action OnTryReload { get; set; }

        public void TryReload()
        {
            OnTryReload?.Invoke();
        }
    }
}