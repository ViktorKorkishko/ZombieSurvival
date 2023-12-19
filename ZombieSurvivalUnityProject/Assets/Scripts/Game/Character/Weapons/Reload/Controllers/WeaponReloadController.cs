using System;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Reload.Models;
using Game.Inputs.Models;
using Zenject;

namespace Game.Character.Weapons.Reload.Controllers
{
    public class WeaponReloadController : IInitializable, IDisposable, ITickable
    {
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        
        private Game.Weapons.Reload.Models.WeaponReloadModel Weapon { get; set; }

        void IInitializable.Initialize()
        {
            CurrentWeaponModel.OnWeaponSet += HandleOnCurrentWeaponSet;
            WeaponReloadModel.OnTryReload += HandleOnTryReload;
        }

        void IDisposable.Dispose()
        {
            CurrentWeaponModel.OnWeaponSet -= HandleOnCurrentWeaponSet;
            WeaponReloadModel.OnTryReload -= HandleOnTryReload;
        }

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool reloadButtonClicked = InputModel.ReloadButtonClickInput;
            if (reloadButtonClicked)
            {
                WeaponReloadModel.TryReload();
            }
        }

        private void HandleOnCurrentWeaponSet(DiContainer weaponContainer)
        {
            Weapon = CurrentWeaponModel.IsWeaponEquipped ? 
                weaponContainer.Resolve<Game.Weapons.Reload.Models.WeaponReloadModel>() : 
                null;
        }

        private void HandleOnTryReload()
        {
            bool weaponEquipped = CurrentWeaponModel.IsWeaponEquipped;
            if (!weaponEquipped)
                return;
            
            Weapon.TryStartReload();
        }
    }
}
