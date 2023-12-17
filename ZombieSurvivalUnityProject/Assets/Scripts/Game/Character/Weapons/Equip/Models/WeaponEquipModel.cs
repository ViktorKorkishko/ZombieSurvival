using System;
using Game.Weapons.Common.Models;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class WeaponEquipModel : MonoBehaviour
    {
        public Action<WeaponModel> OnWeaponEquipped { get; set; }
        public Action<WeaponModel> OnWeaponUnequipped { get; set; }
        
        public void Equip(WeaponModel weaponModel)
        {
            OnWeaponEquipped?.Invoke(weaponModel);
        }
        
        public void Unequip(WeaponModel weaponModel)
        {
            OnWeaponUnequipped?.Invoke(weaponModel);
        }
    }
}
