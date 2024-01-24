using System;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.Shoot.Models;
using Game.Inputs.Models;
using Zenject;

namespace Game.Character.Weapons.Shoot.Controllers
{
    public class CharacterWeaponShootController : IInitializable, ITickable, IDisposable
    {
        [Inject] private CharacterWeaponShootModel CharacterWeaponShootModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }

        private Game.Weapons.Shoot.Models.WeaponShootModel Weapon { get; set; }

        void IInitializable.Initialize()
        {
            CurrentWeaponModel.OnWeaponSet += HandleOnCurrentWeaponSet;
            CharacterWeaponShootModel.OnTryShoot += HandleOnShoot;
        }

        void IDisposable.Dispose()
        {
            CurrentWeaponModel.OnWeaponSet -= HandleOnCurrentWeaponSet;
            CharacterWeaponShootModel.OnTryShoot -= HandleOnShoot;
        }
        
        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleOnCurrentWeaponSet(EquippedWeapon equippedWeapon)
        {
            Weapon = CurrentWeaponModel.IsWeaponEquipped ? 
                equippedWeapon.GetComponent<Game.Weapons.Shoot.Models.WeaponShootModel>() : 
                null;
        }
        
        private void HandleInput()
        {
            bool fireButtonClicked = InputModel.LeftMouseButtonHold;
            if (fireButtonClicked)
            {
                CharacterWeaponShootModel.TryShoot();
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
