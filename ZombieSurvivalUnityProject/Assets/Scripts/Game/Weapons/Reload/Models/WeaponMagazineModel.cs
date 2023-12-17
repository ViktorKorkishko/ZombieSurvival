using System;
using Game.Weapons.Shooting.Config;
using Game.Weapons.Shooting.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Models
{
    public class WeaponMagazineModel : IInitializable, IDisposable
    {
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private WeaponShootingModel WeaponShootingModel { get; }
        [Inject] private WeaponShootingConfig WeaponConfig { get; }

        public int BulletsLeft { get; private set; }

        public void Initialize()
        {
            WeaponShootingModel.OnShoot += HandleOnShoot;
            WeaponReloadModel.OnReloadingEnded += HandleOnReloadingEnded;

            FillMagazine();
        }

        public void Dispose()
        {
            WeaponShootingModel.OnShoot -= HandleOnShoot;
            WeaponReloadModel.OnReloadingEnded -= HandleOnReloadingEnded;
        }

        private void FillMagazine()
        {
            BulletsLeft = WeaponConfig.MagazineSize;
        }

        private void HandleOnShoot()
        {
            BulletsLeft--;
            
            Debug.Log($"BulletsLeft: {BulletsLeft}");
        }

        private void HandleOnReloadingEnded()
        {
            FillMagazine();
            Debug.Log($"BulletsLeft: {BulletsLeft}");
        }
    }
}
