using System;
using Game.Weapons.Facade;

namespace Game.Character.Weapons.PickUp.Models
{
    public class CharacterWeaponPickUpModel
    {
        public Action<WeaponFacade> OnWeaponPickedUp { get; set; }

        public void PickUp(WeaponFacade weapon)
        {
            OnWeaponPickedUp?.Invoke(weapon);
        }
    }
}
