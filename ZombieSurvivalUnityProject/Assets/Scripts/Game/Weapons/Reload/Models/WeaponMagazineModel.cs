using System;
using Game.Weapons.Common.Config;
using Game.Weapons.Shoot.Models;
using Zenject;

namespace Game.Weapons.Reload.Models
{
    public class WeaponMagazineModel : IInitializable, IDisposable
    {
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private WeaponShootModel WeaponShootModel { get; }
        [Inject] private WeaponConfig WeaponConfig { get; }
        
        public int BulletsLeft { get; private set; }

        private int MaxBullets => WeaponConfig.MagazineSize;

        void IInitializable.Initialize()
        {
            WeaponShootModel.OnShoot += HandleOnShoot;
            WeaponReloadModel.OnReloadEnded += HandleOnReloadingEnded;
        }
        
        void IDisposable.Dispose()
        {
            WeaponShootModel.OnShoot -= HandleOnShoot;
            WeaponReloadModel.OnReloadEnded -= HandleOnReloadingEnded;
        }
        
        public void LoadBullets(int bulletsCount)
        {
            if (bulletsCount > MaxBullets)
            {
                BulletsLeft = MaxBullets;
            }
            else
            {
                BulletsLeft = bulletsCount;
            }
        }

        private void HandleOnShoot()
        {
            BulletsLeft--;
        }

        private void HandleOnReloadingEnded()
        {
            LoadBullets(MaxBullets);
        }
    }
}
