using System;
using Game.Character.Weapons.Equip.Models;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public class CurrentWeaponModel
    {
        public EquippedWeapon EquippedWeapon { get; private set; }
        public bool IsWeaponEquipped => EquippedWeapon != null;
        
        public Action<EquippedWeapon> OnWeaponSet { get; set; }

        public void SetCurrentWeapon(EquippedWeapon equippedWeapon)
        {
            EquippedWeapon = equippedWeapon;
            OnWeaponSet?.Invoke(equippedWeapon);
        }
    }
}
