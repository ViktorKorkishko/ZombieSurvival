using Game.Cameras.Models;
using Game.Character.Interaction.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.InteractableObjects.Common.Enums;
using Game.InteractableObjects.Common.Models;
using Game.InteractableObjects.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Interaction.Controllers
{
    public class CharacterObjectInteractionController : ITickable
    {
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private CameraModel CameraModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CharacterObjectInteractionModel CharacterObjectInteractionModel { get; }

        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool pickUpWeaponInput = InputModel.PickUpWeaponButtonClickInput;
            if (!pickUpWeaponInput)
                return;
            
            var interactableObjectView = CheckForObjectsToInteract();
            bool foundInteractableObject = interactableObjectView != null;
            if (!foundInteractableObject)
            {
                return;
            }
            
            HandleInteractableObject(interactableObjectView);
        }

        private InteractableObjectView CheckForObjectsToInteract()
        {
            Vector3 origin = MainCameraTransform.position;
            Vector3 direction = MainCameraTransform.forward;
            float rayDistance = CharacterObjectInteractionModel.MaxPickUpDistance;

            if (Physics.Raycast(origin, direction, out var raycastHit, rayDistance))
            {
                if (raycastHit.collider.TryGetComponent<InteractableObjectView>(out var interactableObjectView))
                {
                    CharacterObjectInteractionModel.DetectInteractableObject(interactableObjectView);
                    return interactableObjectView;
                }
            }

            return null;
        }

        private void HandleInteractableObject(InteractableObjectView interactableObjectView)
        {
            var container = interactableObjectView.Context.Container;
            var model = container.TryResolve<InteractableObjectModel>();
            
            bool successfullyResolved = model != null;
            if (!successfullyResolved)
            {
                Debug.LogError($"Cannot Resolve {typeof(InteractableObjectModel)}");
                return;
            }
            
            var objectType = model.Type;
            switch (objectType)
            {
                case InteractableObjectType.Common:
                    model.Interact();
                    break;
                
                case InteractableObjectType.Weapon:
                    
                    WeaponPickUpModel.PickUp(container);
                    break;
                
                default:
                    Debug.LogError($"Unknown interactable object type! No handler for {objectType}");
                    break;
            }
        }
    }
}
