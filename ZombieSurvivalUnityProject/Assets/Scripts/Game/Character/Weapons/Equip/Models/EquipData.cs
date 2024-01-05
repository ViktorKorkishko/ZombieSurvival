using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquipData
    {
        public Transform WeaponRoot { get; }
        
        public EquipData(Transform weaponRoot)
        {
            WeaponRoot = weaponRoot;
        }
    }
}
