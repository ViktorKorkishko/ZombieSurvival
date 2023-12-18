using UnityEngine;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquipData
    {
        public Transform LeftHandGripTransform { get; }
        public Transform RightHandGripTransform { get; }
        
        public EquipData(Transform leftHandGripTransform, Transform rightHandGripTransform)
        {
            LeftHandGripTransform = leftHandGripTransform;
            RightHandGripTransform = rightHandGripTransform;
        }
    }
}
