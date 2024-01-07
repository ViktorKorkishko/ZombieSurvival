using Game.Weapons.Common;
using UnityEngine;

namespace Game.Animations
{
    [CreateAssetMenu(fileName = "WeaponAnimationsContainer", menuName = "Editor/Animator/WeaponAnimationsContainer")]
    public class WeaponAnimationsContainer : ScriptableObject
    {
        [SerializeField] private WeaponId _weaponId;
        
        [SerializeField] private string _equipAnimatorStateName;
        [SerializeField] private string _sprintingAnimatorStateName;
        
        public WeaponId WeaponId => _weaponId;

        public string EquipAnimatorStateName => _equipAnimatorStateName;
        public string SprintingAnimatorStateName => _sprintingAnimatorStateName;
    }
}
