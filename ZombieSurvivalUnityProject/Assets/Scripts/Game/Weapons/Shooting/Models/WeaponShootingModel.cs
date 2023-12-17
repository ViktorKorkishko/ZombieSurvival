using System;
using Game.Weapons.Reload.Models;
using Zenject;

namespace Game.Weapons.Shooting.Models
{
    public class WeaponShootingModel
    {
        [Inject] private WeaponMagazineModel WeaponMagazineModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        
        public Action OnShoot { get; set; }

        public void TryShoot()
        {
            bool haveBullets = WeaponMagazineModel.BulletsLeft > 0;
            bool canShoot = haveBullets && !WeaponReloadModel.IsRealoding;
            if (!canShoot)
                return;

            OnShoot?.Invoke();
        }
    }
}
