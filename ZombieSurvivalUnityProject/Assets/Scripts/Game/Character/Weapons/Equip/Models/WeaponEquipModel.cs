using System;
using Game.Weapons.Common.Models;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class WeaponEquipModel : MonoBehaviour
    {
        public Action<EquipData> OnWeaponEquipped { get; set; }
        public Action<EquipData> OnWeaponUnequipped { get; set; }
        
        public void Equip(EquipData equipData)
        {
            OnWeaponEquipped?.Invoke(equipData);
        }
        
        public void Unequip(EquipData equipData)
        {
            OnWeaponUnequipped?.Invoke(equipData);
        }
    }
}
