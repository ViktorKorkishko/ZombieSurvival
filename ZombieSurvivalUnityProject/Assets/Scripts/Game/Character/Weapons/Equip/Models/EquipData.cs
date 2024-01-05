using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquipData
    {
        public Transform WeaponRoot { get; }
        public Transform LeftHandGripTransform { get; }
        public Transform RightHandGripTransform { get; }
        
        public EquipData(Transform weaponRoot, 
            Transform leftHandGripTransform, 
            Transform rightHandGripTransform)
        {
            WeaponRoot = weaponRoot;
            LeftHandGripTransform = leftHandGripTransform;
            RightHandGripTransform = rightHandGripTransform;
        }
    }
}
