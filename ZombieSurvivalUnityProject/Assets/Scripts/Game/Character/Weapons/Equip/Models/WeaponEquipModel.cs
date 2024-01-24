using System;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class WeaponEquipModel : MonoBehaviour
    {
        public Action<EquippedWeapon> OnWeaponEquipped { get; set; }
        public Action OnWeaponUnequipped { get; set; }
        
        public void Equip(EquippedWeapon equippedWeapon)
        {
            OnWeaponEquipped?.Invoke(equippedWeapon);
        }
        
        public void Unequip()
        {
            OnWeaponUnequipped?.Invoke();
        }
    }
}
