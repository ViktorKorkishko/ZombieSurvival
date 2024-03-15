using System;
using Core.Exceptions;
using Core.Installers;
using DG.Tweening;
using Game.InteractableObjects.Common.Controllers;
using Game.InteractableObjects.Implementations.Door.Enums;
using Game.InteractableObjects.Implementations.Door.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.Door.Controllers
{
    public class InteractableDoorController : InteractableObjectControllerBase
    {
        [Inject] private InteractableDoorModel InteractableDoorModel { get; }
        [Inject(Id = BindingIdentifiers.Rigidbody)] private Rigidbody Rigidbody { get; }

        private Action InteractionEndedCallback { get; set; }

        private Sequence _rotationSequence;

        public override void Initialize()
        {
            base.Initialize();
            
            InteractableDoorModel.OnInteractionStarted += HandleOnInteractionStarted;
            InteractableDoorModel.OnInteractionInterrupted += HandleOnInteractionInterrupted;
        }

        public override void Dispose()
        {
            base.Dispose();

            InteractableDoorModel.OnInteractionStarted -= HandleOnInteractionStarted;
            InteractableDoorModel.OnInteractionInterrupted -= HandleOnInteractionInterrupted;
        }

        protected override void HandleOnObjectInteract()
        {
            InteractableDoorModel.Interact();
        }
        
        private void HandleOnInteractionInterrupted(DoorState doorState, Action interactionEndedCallback)
        {
            InteractionEndedCallback = interactionEndedCallback;

            StopRotation();
            
            var targetAngle = GetTargetAngle();
            float tweenProgress = _rotationSequence.position;
            float passedTimeInterpolated = InteractableDoorModel.OpenDuration * tweenProgress;

            RotateToTargetAngle(targetAngle, passedTimeInterpolated);

            Vector3 GetTargetAngle()
            {
                switch (doorState)
                {
                    case DoorState.Opening:
                        return InteractableDoorModel.CloseAngle;

                    case DoorState.Closing:
                        return InteractableDoorModel.OpenAngle;

                    default:
                        Debug.LogError(new EnumNotSupportedException<DoorState>(doorState));
                        return Vector3.zero;
                }
            }
        }

        private void HandleOnInteractionStarted(DoorState doorState, Action interactionEndedCallback)
        {
            InteractionEndedCallback = interactionEndedCallback;

            switch (doorState)
            {
                case DoorState.Opened:
                    CloseDoor();
                    break;

                case DoorState.Closed:
                    OpenDoor();
                    break;

                default:
                    Debug.LogError(new EnumNotSupportedException<DoorState>(doorState));
                    break;
            }
        }

        private void OpenDoor()
        {
            RotateToTargetAngle(InteractableDoorModel.OpenAngle, InteractableDoorModel.OpenDuration);
        }

        private void CloseDoor()
        {
            RotateToTargetAngle(InteractableDoorModel.CloseAngle, InteractableDoorModel.OpenDuration);
        }

        private void RotateToTargetAngle(Vector3 targetAngle, float duration)
        {
            var currentRotationEuler = Rigidbody.transform.localRotation.eulerAngles;
            var rotationAngle = targetAngle - currentRotationEuler;

            _rotationSequence = DOTween.Sequence()
                .Append(GetRotationTween())
                .OnComplete(CompleteCallback);

            Tween GetRotationTween()
            {
                return Rigidbody
                    .DORotate(rotationAngle, duration, RotateMode.LocalAxisAdd)
                    .SetEase(InteractableDoorModel.Ease);
            }

            void CompleteCallback()
            {
                Rigidbody.transform.localRotation = Quaternion.Euler(targetAngle);

                InteractionEndedCallback?.Invoke();
                InteractionEndedCallback = null;
            }
        }

        private void StopRotation()
        {
            _rotationSequence.Kill();
        }
    }
}
