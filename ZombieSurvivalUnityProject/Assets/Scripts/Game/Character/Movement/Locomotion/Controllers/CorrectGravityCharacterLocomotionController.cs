using System;
using Core.Installers;
using Game.Animators.Models;
using Game.Character.Movement.Locomotion.Models;
using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Locomotion.Controllers
{
    public class CorrectGravityCharacterLocomotionController : IInitializable, IDisposable, IFixedTickable, ITickable
    {
        [Inject] private CorrectGravityCharacterLocomotionModel CorrectGravityCharacterLocomotionModel { get; }
        [Inject] private AnimatorCallbacksModel AnimatorCallbacksModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private Animator Animator { get; }
        [Inject] private CharacterController CharacterController { get; }
        [Inject(Id = BindingIdentifiers.JumpParamId)] private string JumpParamName { get; }
        
        private bool _isJumping;
        private int _jumpingParamId => Animator.StringToHash(JumpParamName);

        private float _initialJumpVelocity = 0f;
        private Vector3 _verticalMovement = Vector3.zero;
        
        private Vector3 _animatorVelocity;
        private Vector3 _rootMotion = Vector3.zero;
        
        private readonly float _groundGravity = -0.05f;
        private float _currentJumpGravity = -9.81f;
        
        private readonly Vector3 _zeroVector = new(0, 0, 0);

        void IInitializable.Initialize()
        {
            AnimatorCallbacksModel.OnAnimatorMove += HandleOnAnimatorMove;

            SetupJumpValues();

            void SetupJumpValues()
            {
                float timeToApex = CorrectGravityCharacterLocomotionModel.MaxJumpTime / 2;
                _currentJumpGravity = -2 * CorrectGravityCharacterLocomotionModel.MaxJumpHeight / Mathf.Pow(timeToApex, 2);
                _initialJumpVelocity = 2 * CorrectGravityCharacterLocomotionModel.MaxJumpHeight / timeToApex;
            }
        }

        void IDisposable.Dispose()
        {
            AnimatorCallbacksModel.OnAnimatorMove -= HandleOnAnimatorMove;
        }

        void IFixedTickable.FixedTick()
        {
            HandleMovement();
            HandleGravity();
            HandleJump();
        }

        void ITickable.Tick()
        {
            bool jumpButtonPressed = InputModel.JumpButtonClickInput;
            if (jumpButtonPressed)
            {
                TryJump();
            }
        }

        private void TryJump()
        {
            bool grounded = CharacterController.isGrounded;
            if (!grounded)
                return;

            if (_isJumping)
                return;

            Jump();
                
            void Jump()
            {
                SetInAir(_initialJumpVelocity);
                Animator.SetBool(_jumpingParamId, true);
            }
        }

        private void HandleMovement()
        {
            if (_isJumping)
            {
                UpdateMovementInAir();
            }
            else
            {
                UpdateMovementOnGround();
            }
        
            void UpdateMovementOnGround()
            {
                Vector3 horizontalMovement = _rootMotion * CorrectGravityCharacterLocomotionModel.GroundSpeed;
                Vector3 verticalMovement = _verticalMovement * Time.fixedDeltaTime;
                Vector3 currentMovement = horizontalMovement + verticalMovement;

                CharacterController.Move(currentMovement);
                _rootMotion = _zeroVector;

                bool isGrounded = CharacterController.isGrounded;
                if (!isGrounded)
                {
                    var initialJumpVelocity = 0f;
                    SetInAir(initialJumpVelocity);
                    Animator.SetBool(_jumpingParamId, true);
                }
            }

            void UpdateMovementInAir()
            {
                Vector3 horizontalMovement = CalculateAirHorizontalMovement();
                Vector3 verticalMovement = _animatorVelocity * Time.fixedDeltaTime;
                Vector3 currentMovement = horizontalMovement + verticalMovement;
                
                CharacterController.Move(currentMovement);
                
                _isJumping = !CharacterController.isGrounded;
                _rootMotion = _zeroVector;
            
                Animator.SetBool(_jumpingParamId, _isJumping);

                Vector3 CalculateAirHorizontalMovement()
                {
                    var multiplier = CorrectGravityCharacterLocomotionModel.AirHorizontalMovementMultiplier;
                    var transform = CharacterController.transform;
                    return (transform.right * InputModel.HorizontalAxisInput + 
                            transform.forward * InputModel.VerticalAxisInput) * multiplier;
                }
            }
        }
        
        private void SetInAir(float initialJumpVelocity)
        {
            _isJumping = true;
            _animatorVelocity = Animator.velocity * CorrectGravityCharacterLocomotionModel.GroundSpeed;
            _animatorVelocity.y = initialJumpVelocity;
        }

        private void HandleGravity()
        {
            _animatorVelocity.y += _currentJumpGravity * Time.fixedDeltaTime;
            
            _verticalMovement.y = CharacterController.isGrounded ? 
                _groundGravity / Time.fixedDeltaTime : 
                _verticalMovement.y + _currentJumpGravity * Time.fixedDeltaTime;
        }

        private void HandleJump()
        {
            bool grounded = CharacterController.isGrounded;
            if (grounded)
            {
                _isJumping = false;
            }
        }

        private void HandleOnAnimatorMove()
        {
            _rootMotion += Animator.deltaPosition;
        }
    }
}
