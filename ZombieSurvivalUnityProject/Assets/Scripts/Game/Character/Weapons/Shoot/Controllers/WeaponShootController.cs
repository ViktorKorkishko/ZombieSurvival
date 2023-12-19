using System;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Shoot.Models;
using Game.Inputs.Models;
using Zenject;

namespace Game.Character.Weapons.Shoot.Controllers
{
    public class WeaponShootController : IInitializable, ITickable, IDisposable
    {
        [Inject] private WeaponShootModel WeaponShootModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }

        private Game.Weapons.Shoot.Models.WeaponShootModel Weapon { get; set; }

        void IInitializable.Initialize()
        {
            CurrentWeaponModel.OnWeaponSet += HandleOnCurrentWeaponSet;
            WeaponShootModel.OnTryShoot += HandleOnShoot;
        }

        void IDisposable.Dispose()
        {
            CurrentWeaponModel.OnWeaponSet -= HandleOnCurrentWeaponSet;
            WeaponShootModel.OnTryShoot -= HandleOnShoot;
        }
        
        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleOnCurrentWeaponSet(DiContainer weaponContainer)
        {
            Weapon = CurrentWeaponModel.IsWeaponEquipped ? 
                weaponContainer.Resolve<Game.Weapons.Shoot.Models.WeaponShootModel>() : 
                null;
        }
        
        private void HandleInput()
        {
            bool fireButtonClicked = InputModel.LeftMouseButtonClicked;
            if (fireButtonClicked)
            {
                WeaponShootModel.TryShoot();
            }
        }
        
        private void HandleOnShoot()
        {
            bool weaponEquipped = CurrentWeaponModel.IsWeaponEquipped;
            if (!weaponEquipped)
                return;
            
            Weapon.TryShoot();
        }
    }
}
