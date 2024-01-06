using Game.Weapons.Common;
using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquipData
    {
        public Transform WeaponRoot { get; }
        public WeaponId WeaponId { get; }

        public EquipData(Transform weaponRoot)
        {
            WeaponRoot = weaponRoot;
        }
    }
}
