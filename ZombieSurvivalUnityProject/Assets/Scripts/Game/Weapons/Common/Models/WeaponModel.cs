using System;

namespace Game.Weapons.Common.Models
{
    public class WeaponModel
    {
        public Action OnTryShoot { get; set; }
        public Action OnTryReload { get; set; }

        public void TryShoot()
        {
            OnTryShoot?.Invoke();
        }

        public void TryReload()
        {
            OnTryReload?.Invoke();
        }
    }
}