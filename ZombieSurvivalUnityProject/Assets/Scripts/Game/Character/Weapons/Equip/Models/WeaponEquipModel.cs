using System;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class WeaponEquipModel : MonoBehaviour
    {
        public Action<EquipData> OnWeaponEquipped { get; set; }
        public Action OnWeaponUnequipped { get; set; }
        
        public void Equip(EquipData equipData)
        {
            OnWeaponEquipped?.Invoke(equipData);
        }
        
        public void Unequip()
        {
            OnWeaponUnequipped?.Invoke();
        }
    }
}
