using System;
using Game.Weapons.Facade;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class CharacterWeaponEquipModel : MonoBehaviour
    {
        public Action<WeaponFacade> OnWeaponEquipped { get; set; }
        public Action OnWeaponUnequipped { get; set; }
        
        public void Equip(WeaponFacade weapon)
        {
            OnWeaponEquipped?.Invoke(weapon);
        }
        
        public void Unequip()
        {
            OnWeaponUnequipped?.Invoke();
        }
    }
}
