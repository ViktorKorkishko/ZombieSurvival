using System;
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

        private Vector3 CurrentMovement = Vector3.zero;
        
        private Vector3 _rootMotion = Vector3.zero;
        private readonly Vector3 _zeroVector = new(0, 0, 0);
        private float initialJumpVelocity = 0f;

        private Vector3 velocity;
        private bool isJumping = false;
        private float stepDown = 0.3f;
        private float airControl = 2.5f;
        private float jumpDemp = 0.5f;
        private float groundSpeed = 1f;

        private readonly float _groundGravity = -0.05f;
        private float _currentJumpGravity = -9.81f;

        void IInitializable.Initialize()
        {
            AnimatorCallbacksModel.OnAnimatorMove += HandleOnAnimatorMove;

            SetupJumpValues();
        }

        void IDisposable.Dispose()
        {
            AnimatorCallbacksModel.OnAnimatorMove -= HandleOnAnimatorMove;
        }

        private void SetupJumpValues()
        {
            float timeToApex = CharacterLocomotionModel.MaxJumpTime / 2;
            _currentJumpGravity = (-2 * CharacterLocomotionModel.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * CharacterLocomotionModel.MaxJumpHeight) / timeToApex;
        }

        void IFixedTickable.FixedTick()
        {
            HandleMovement();
            
            HandleGravity();
        }

        void ITickable.Tick()
        {
            SetupJumpValues();
            
            bool jumpButtonPressed = InputModel.JumpButtonClickInput;
            if (jumpButtonPressed)
            {
                TryJump();
            }
        }
        
        private void TryJump()
        {
            bool grounded = CharacterController.isGrounded;
            if (grounded)
            {
                CurrentMovement.y = initialJumpVelocity;
            }
        }

        private void HandleMovement()
        {
            Vector3 stepForwardAmount = _rootMotion * groundSpeed;

            CharacterController.Move(CurrentMovement * Time.fixedDeltaTime + stepForwardAmount);
            _rootMotion = _zeroVector;
        }

        private void HandleGravity()
        {
            CurrentMovement.y = CharacterController.isGrounded ? 
                _groundGravity / Time.fixedDeltaTime : 
                CurrentMovement.y + _currentJumpGravity * Time.fixedDeltaTime;
        }

        private void HandleOnAnimatorMove()
        {
            _rootMotion += Animator.deltaPosition;
        }
    }
}
