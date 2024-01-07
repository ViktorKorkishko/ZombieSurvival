using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace Game.Animations.Editor
{
    [CreateAssetMenu(fileName = "AnimatorStateNamesValidator", menuName = "Editor/Animator/AnimatorStateNamesValidator")]
    public class AnimatorStatesNamesValidator : ScriptableObject
    {
        [Header("Container")] 
        [SerializeField] private WeaponAnimationsContainer _animationsContainer;

        [Header("Controller")]
        [SerializeField] private AnimatorController _animatorController;

        private List<string> _animationNames = new();

        private void OnValidate()
        {
            InitAnimationsList();
        }

        private void InitAnimationsList()
        {
            _animationNames = new List<string>
            {
                _animationsContainer.EquipAnimatorStateName + _animationsContainer.WeaponId,
                _animationsContainer.SprintingAnimatorStateName + _animationsContainer.WeaponId,
            };
        }
        
        // run when entering play mode
        private void ValidateAnimations()
        {
            var statesNames = GetStatesNames();
            var validationSuccess = _animationNames.All(validatingAnimation => statesNames.Any(stateName => stateName == validatingAnimation));
            
            // disable play mode if not validated
        }

        [ContextMenu("GetStatesNames")]
        private IEnumerable<string> GetStatesNames()
        {
            var statesNames = _animatorController.layers
                .Select(animatorControllerLayer => animatorControllerLayer.stateMachine.states)
                .Select(childAnimatorStates => childAnimatorStates.Select(x => x.state.name))
                .SelectMany(state => state)
                .ToList();

            return statesNames;
        }
    }
}
