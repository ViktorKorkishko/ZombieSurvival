using System;
using Game.Weapons.Common.Config;
using Game.Weapons.Shoot.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Models
{
    public class WeaponMagazineModel : IInitializable, IDisposable
    {
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private WeaponShootModel WeaponShootModel { get; }
        [Inject] private WeaponConfig WeaponConfig { get; }

        public int BulletsLeft { get; private set; }

        public void Initialize()
        {
            WeaponShootModel.OnShoot += HandleOnShoot;
            WeaponReloadModel.OnReloadEnded += HandleOnReloadingEnded;

            FillMagazine();
        }

        public void Dispose()
        {
            WeaponShootModel.OnShoot -= HandleOnShoot;
            WeaponReloadModel.OnReloadEnded -= HandleOnReloadingEnded;
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
