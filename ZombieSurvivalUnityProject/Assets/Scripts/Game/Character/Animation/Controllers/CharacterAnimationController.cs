using Core.Installers;
using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Animation.Controllers
{
    public class CharacterAnimationController : ITickable
    {
        [Inject(Id = BindingIdentifiers.CharacterLocomotionAnimator)] private Animator CharacterLocomotionAnimator { get; }
        [Inject] private InputModel InputModel { get; }

        private Vector2 _input;
        
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");

        void ITickable.Tick()
        {
            _input.x = InputModel.HorizontalAxisInput;
            _input.y = InputModel.VerticalAxisInput;
            
            CharacterLocomotionAnimator.SetFloat(InputX, _input.x);
            CharacterLocomotionAnimator.SetFloat(InputY, _input.y);
        }
    }
}
