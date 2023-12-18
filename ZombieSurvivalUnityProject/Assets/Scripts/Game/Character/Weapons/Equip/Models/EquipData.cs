using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquipData
    {
        public Transform WeaponWeaponRoot { get; }
        public Transform LeftHandGripTransform { get; }
        public Transform RightHandGripTransform { get; }
        
        public EquipData(Transform weaponRoot, 
            Transform leftHandGripTransform, 
            Transform rightHandGripTransform)
        {
            WeaponWeaponRoot = weaponRoot;
            LeftHandGripTransform = leftHandGripTransform;
            RightHandGripTransform = rightHandGripTransform;
        }
    }
}
