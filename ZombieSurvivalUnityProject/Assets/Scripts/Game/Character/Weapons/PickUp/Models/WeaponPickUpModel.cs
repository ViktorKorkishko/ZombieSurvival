using System;
using Zenject;

namespace Game.Character.Weapons.PickUp.Models
{
    public class WeaponPickUpModel
    {
        public Action<DiContainer> OnWeaponPickedUp { get; set; }

        public void PickUp(DiContainer weaponContainer)
        {
            OnWeaponPickedUp?.Invoke(weaponContainer);
        }
    }
}
