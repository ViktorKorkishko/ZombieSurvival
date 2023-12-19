using System;

namespace Game.Weapons.Equip.Models
{
    public class WeaponEquipModel
    {
        public Action OnEquipped { get; set; }
        public Action OnUnequipped { get; set; }

        public void Equip()
        {
            OnEquipped?.Invoke();
        }

        public void Unequip()
        {
            OnUnequipped?.Invoke();
        }
    }
}