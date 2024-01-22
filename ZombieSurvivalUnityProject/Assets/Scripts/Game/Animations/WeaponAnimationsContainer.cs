using Game.Weapons.Common;
using UnityEngine;

namespace Game.Animations
{
    [CreateAssetMenu(fileName = "WeaponAnimationsContainer", menuName = "Editor/Animator/WeaponAnimationsContainer")]
    public class WeaponAnimationsContainer : ScriptableObject
    {
        [SerializeField] private WeaponId _weaponId;
        
        [Header("State names")]
        [SerializeField] private string _equipAnimatorStateName;
        [SerializeField] private string _sprintingAnimatorStateName;
        
        [Header("Params")]
        [SerializeField] private string _reloadAnimationTriggerName;
        
        public WeaponId WeaponId => _weaponId;

        public string EquipAnimatorStateName => _equipAnimatorStateName;
        public string SprintingAnimatorStateName => _sprintingAnimatorStateName;
        public string ReloadAnimationTriggerName => _reloadAnimationTriggerName;
    }
}
