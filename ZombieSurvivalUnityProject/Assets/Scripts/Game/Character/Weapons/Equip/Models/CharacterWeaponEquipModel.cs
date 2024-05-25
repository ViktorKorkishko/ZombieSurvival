using System;
using Game.Weapons.Facade;

namespace Game.Character.Weapons.Equip.Models
{
    public class CharacterWeaponEquipModel
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
