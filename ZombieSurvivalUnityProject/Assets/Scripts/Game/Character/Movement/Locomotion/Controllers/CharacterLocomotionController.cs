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

        private Vector3 _rootMotion = Vector3.zero;
        private readonly Vector3 _zeroVector = new(0, 0, 0);

        private Vector3 velocity;
        private bool isJumping;
        private float stepDown = 0.3f;
        private float airControl = 2.5f;
        private float jumpDemp = 0.5f;
        private float groundSpeed = 1f;
        
        private float Gravity => Physics.gravity.y;

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
            if (isJumping)
            {
                UpdateInAir();
            }
            else // is grounded state
            {
                UpdateOnGround();
            }
        }

        private void UpdateInAir()
        {
            velocity.y += Gravity * Time.fixedDeltaTime;
            Vector3 displacement = velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControl();
            CharacterController.Move(displacement);
            isJumping = !CharacterController.isGrounded;
            _rootMotion = _zeroVector;
            
            Animator.SetBool("IsJumping", isJumping);
        }

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _rootMotion * groundSpeed;
            Vector3 stepDownAmount = Vector3.down * stepDown;
            
            CharacterController.Move(stepForwardAmount + stepDownAmount);
            _rootMotion = _zeroVector;

            if (!CharacterController.isGrounded)
            {
                isJumping = true;
                velocity = Animator.velocity * jumpDemp;
                velocity.y = 0;
                Animator.SetBool("IsJumping", true);
            }
        }

        void ITickable.Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        void Jump()
        {
            if (isJumping)
                return;

            isJumping = true;
            velocity = Animator.velocity * jumpDemp * groundSpeed;
            velocity.y = Mathf.Sqrt(2 * -Gravity * CharacterLocomotionModel.JumpHeight);
            Animator.SetBool("IsJumping", true);
        }

        Vector3 CalculateAirControl()
        {
            var transform = CharacterController.transform;
            return ((transform.forward * InputModel.VerticalAxisInput) + (transform.right * InputModel.HorizontalAxisInput)) * (airControl / 100);
        }

        private void HandleOnAnimatorMove()
        {
            _rootMotion += Animator.deltaPosition;
        }
    }
}