using System;
using Game.Weapons.Common.Models;
using Game.Weapons.Common.Views;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shooting.Models;
using Zenject;

namespace Game.Weapons.Common.Controllers
{
    public class WeaponController : IInitializable, IDisposable
    {
        [Inject] private WeaponView WeaponView { get; }
        [Inject] private WeaponModel WeaponModel { get; }

        [Inject] private WeaponShootingModel WeaponShootingModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }

        void IInitializable.Initialize()
        {
            WeaponModel.OnTryShoot += HandleOnTryShoot;
            WeaponModel.OnTryReload += HandleOnTryReload;
            WeaponView.OnWeaponPickedUp += HandleOnWeaponPickedUp;
        }

        void IDisposable.Dispose()
        {
            WeaponModel.OnTryShoot -= HandleOnTryShoot;
            WeaponModel.OnTryReload -= HandleOnTryReload;
            WeaponView.OnWeaponPickedUp -= HandleOnWeaponPickedUp;
        }

        private WeaponModel HandleOnWeaponPickedUp()
        {
            return WeaponModel;
        }

        private void HandleOnTryShoot()
        {
            WeaponShootingModel.TryShoot();
        }

        private void HandleOnTryReload()
        {
            WeaponReloadModel.TryReload();
        }
    }
}