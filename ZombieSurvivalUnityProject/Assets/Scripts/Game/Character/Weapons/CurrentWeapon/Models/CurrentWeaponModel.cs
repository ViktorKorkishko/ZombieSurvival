using System;
using Game.Character.Weapons.Equip.Models;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public class CurrentWeaponModel
    {
        public EquippedWeapon Weapon { get; private set; }
        public bool IsWeaponEquipped => Weapon != null;
        
        public Action<EquippedWeapon> OnWeaponSet { get; set; }

        public void SetCurrentWeapon(EquippedWeapon equippedWeapon)
        {
            Weapon = equippedWeapon;
            OnWeaponSet?.Invoke(equippedWeapon);
        }
    }
}
