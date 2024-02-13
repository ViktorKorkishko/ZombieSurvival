using System;
using System.Collections;
using Core.Coroutines.Models;
using Core.Installers;
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
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }

        private Action InteractionEndedCallback { get; set; }

        private int _coroutineIndex = -1;

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
                    Debug.LogError("Cannot handle such");
                    break;
            }
        }

        private void OpenDoor()
        {
            Debug.Log("Open door");
            _coroutineIndex = 
                CoroutinePlayerModel.StartCoroutine(RotateToTargetAngle(InteractableDoorModel.OpenAngle));
        }

        private void CloseDoor()
        {
            Debug.Log("Closing door");
            _coroutineIndex =
                CoroutinePlayerModel.StartCoroutine(RotateToTargetAngle(InteractableDoorModel.CloseAngle));
        }

        private IEnumerator RotateToTargetAngle(Vector3 targetAngle)
        {
            float currentRotationTime = 0;
            var rotationAngle = targetAngle - Rigidbody.transform.localRotation.eulerAngles;
            float rotationEndTime = InteractableDoorModel.OpenTime;

            while (currentRotationTime <= rotationEndTime)
            {
                yield return new WaitForFixedUpdate();

                var anglePerSecond = rotationAngle / InteractableDoorModel.OpenTime;
                Quaternion deltaRotation = Quaternion.Euler(anglePerSecond * Time.fixedDeltaTime);
                Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);

                currentRotationTime += Time.fixedDeltaTime;
            }
            
            Rigidbody.transform.localRotation = Quaternion.Euler(targetAngle);
            
            InteractionEndedCallback?.Invoke();
            InteractionEndedCallback = null;
        }
    }
}