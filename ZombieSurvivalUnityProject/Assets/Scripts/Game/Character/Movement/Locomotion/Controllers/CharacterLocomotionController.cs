using System;
using Core.Installers;
using Game.Animators.Models;
using Game.Character.Movement.Locomotion.Models;
using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Locomotion.Controllers
{
    public class CharacterLocomotionController : IInitializable, IDisposable, IFixedTickable, ITickable
    {
        [Inject] private CharacterLocomotionModel CharacterLocomotionModel { get; }
        [Inject] private AnimatorCallbacksModel AnimatorCallbacksModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private Animator Animator { get; }
        [Inject] private CharacterController CharacterController { get; }
        [Inject(Id = BindingIdentifiers.JumpParamId)] private string JumpParamName { get; }
        [Inject(Id = BindingIdentifiers.SprintParamId)] private string SprintParamName { get; }

        private Vector3 _animatorVelocity;
        private Vector3 _rootMotion;
        private readonly Vector3 _zeroVector = new(0, 0, 0);
        
        private bool _isJumping;
        private int IsJumping => Animator.StringToHash(JumpParamName);
        private int IsSprinting => Animator.StringToHash(SprintParamName);

        void IInitializable.Initialize()
        {
            AnimatorCallbacksModel.OnAnimatorMove += HandleOnAnimatorMove;
        }

        void IDisposable.Dispose()
        {
            AnimatorCallbacksModel.OnAnimatorMove -= HandleOnAnimatorMove;
        }

        void IFixedTickable.FixedTick()
        {
            if (_isJumping)
            {
                UpdateInAir();
            }
            else // is grounded state
            {
                UpdateOnGround();
            }
        }

        void ITickable.Tick()
        {
            bool jumpButtonPressed = InputModel.JumpButtonClickInput;
            if (jumpButtonPressed)
            {
                TryJump();
            }
            
            bool sprintButtonPressed = InputModel.SprintButtonHoldInput;
            SetIsSprinting(sprintButtonPressed);
        }

        private void UpdateInAir()
        {
            _animatorVelocity.y += CharacterLocomotionModel.AirGravity * Time.fixedDeltaTime;
            Vector3 movement = _animatorVelocity * Time.fixedDeltaTime;
            movement += CalculateAirControl();

            CharacterController.Move(movement);

            _isJumping = !CharacterController.isGrounded;
            _rootMotion = _zeroVector;

            Animator.SetBool(IsJumping, _isJumping);

            Vector3 CalculateAirControl()
            {
                var transform = CharacterController.transform;
                return (transform.forward * InputModel.VerticalAxisInput +
                        transform.right * InputModel.HorizontalAxisInput) * CharacterLocomotionModel.AirControlMultiplier;
            }
        }

        private void UpdateOnGround()
        {
            Vector3 horizontalMovement = _rootMotion * CharacterLocomotionModel.GroundSpeed;
            Vector3 verticalMovement = Vector3.up * CharacterLocomotionModel.GroundGravity;

            CharacterController.Move(horizontalMovement + verticalMovement);
            _rootMotion = _zeroVector;

            if (!CharacterController.isGrounded)
            {
                float initialJumpVelocity = 0f;
                SetInAir(initialJumpVelocity);
                Animator.SetBool(IsJumping, true);
            }
        }

        private void TryJump()
        {
            if (_isJumping)
                return;
            
            Jump();
            
            void Jump()
            {
                float initialJumpVelocity = Mathf.Sqrt(-2 * CharacterLocomotionModel.AirGravity * CharacterLocomotionModel.JumpHeight);
                SetInAir(initialJumpVelocity);
                Animator.SetBool(IsJumping, true);
            }
        }

        private void SetInAir(float initialJumpVelocity)
        {
            _isJumping = true;
            _animatorVelocity = Animator.velocity * CharacterLocomotionModel.JumpDemping * CharacterLocomotionModel.GroundSpeed;
            _animatorVelocity.y = initialJumpVelocity;
        }

        private void SetIsSprinting(bool isSprinting)
        {
            Animator.SetBool(IsSprinting, isSprinting);
        }

        private void HandleOnAnimatorMove()
        {
            _rootMotion += Animator.deltaPosition;
        }
    }
}
