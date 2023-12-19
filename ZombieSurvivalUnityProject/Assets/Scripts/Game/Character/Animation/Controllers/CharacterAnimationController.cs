using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Animation.Controllers
{
    public class CharacterAnimationController : ITickable
    {
        [Inject] private Animator Animator { get; }
        [Inject] private InputModel InputModel { get; }

        private Vector2 _input;

        void ITickable.Tick()
        {
            _input.x = InputModel.HorizontalAxisInput;
            _input.y = InputModel.VerticalAxisInput;
            
            Animator.SetFloat("InputX", _input.x);
            Animator.SetFloat("InputY", _input.y);
        }
    }
}
