using System;

namespace Game.Weapons.Shoot.Models
{
    public class WeaponShootModel
    {
        public Func<bool> OnTryShoot { get; set; }
        public Action OnShoot { get; set; }

        public void TryShoot()
        {
            bool canShoot = OnTryShoot?.Invoke() ?? false;

            if (canShoot)
            {
                OnShoot?.Invoke();
            }
        }
    }
}
