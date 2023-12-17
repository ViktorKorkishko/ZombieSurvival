using System;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.Weapons.Common.Views;
using Zenject;

namespace Game.Character.Weapons.PickUp.Controllers
{
    public class WeaponPickUpController : IInitializable, IDisposable, ITickable
    {
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }

        void IInitializable.Initialize()
        {

        }

        void IDisposable.Dispose()
        {

        }

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool pickUpWeapon = InputModel.PickUpWeaponButtonClickInput;
            if (pickUpWeapon)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            // temp (for test)
            var weaponView = UnityEngine.Object.FindObjectOfType<WeaponView>();
            var pickedWeapon = weaponView.PickUp();
            
            WeaponEquipModel.Equip(pickedWeapon);
        }
    }
}
