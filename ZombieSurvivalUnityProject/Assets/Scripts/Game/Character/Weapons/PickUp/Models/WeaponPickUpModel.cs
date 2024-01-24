using System;
using Game.Character.Weapons.Equip.Models;

namespace Game.Character.Weapons.PickUp.Models
{
    public class WeaponPickUpModel
    {
        public Action<EquippedWeapon> OnWeaponPickedUp { get; set; }

        public void PickUp(EquippedWeapon equippedWeapon)
        {
            OnWeaponPickedUp?.Invoke(equippedWeapon);
        }
    }
}
