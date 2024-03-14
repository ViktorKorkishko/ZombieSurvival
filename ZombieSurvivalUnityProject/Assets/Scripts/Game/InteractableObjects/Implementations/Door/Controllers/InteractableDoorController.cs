using System;
using System.Collections;
using Core.Coroutines.Models;
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

        public override void Initialize()
        {
            base.Initialize();

            InteractableDoorModel.OnInteractionStarted += HandleOnDoorInteract;
        }

        public override void Dispose()
        {
            base.Dispose();

            InteractableDoorModel.OnInteractionStarted -= HandleOnDoorInteract;
        }

        protected override void HandleOnObjectInteract()
        {
            InteractableDoorModel.Interact();
        }

        private void HandleOnDoorInteract(DoorState doorState, Action interactionEndedCallback)
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
            RotateToTargetAngle(InteractableDoorModel.OpenAngle);
        }

        private void CloseDoor()
        {
            RotateToTargetAngle(InteractableDoorModel.CloseAngle);
        }

        private void RotateToTargetAngle(Vector3 targetAngle)
        {
            var currentRotationEuler = Rigidbody.transform.localRotation.eulerAngles;
            var rotationAngle = targetAngle - currentRotationEuler;
            float time = InteractableDoorModel.OpenTime;

            Sequence rotationSequence = DOTween.Sequence();
            rotationSequence
                .Append(Rigidbody.DORotate(rotationAngle, time, RotateMode.LocalAxisAdd))
                .OnComplete(() =>
                {
                    Rigidbody.transform.localRotation = Quaternion.Euler(targetAngle);

                    InteractionEndedCallback?.Invoke();
                    InteractionEndedCallback = null;
                });
        }
    }
}