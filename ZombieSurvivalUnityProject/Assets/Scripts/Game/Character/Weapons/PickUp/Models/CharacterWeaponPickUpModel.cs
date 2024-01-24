using System;
using Game.Character.Weapons.Equip.Models;

namespace Game.Character.Weapons.PickUp.Models
{
    public class CharacterWeaponPickUpModel
    {
        public Action<EquippedWeapon> OnWeaponPickedUp { get; set; }

        public void PickUp(EquippedWeapon equippedWeapon)
        {
            OnWeaponPickedUp?.Invoke(equippedWeapon);
        }
    }
}
