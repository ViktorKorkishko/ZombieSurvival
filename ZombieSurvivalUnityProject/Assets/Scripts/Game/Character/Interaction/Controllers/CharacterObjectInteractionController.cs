using Game.Cameras.Models;
using Game.Character.Interaction.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.InteractableObjects.Common.Enums;
using Game.InteractableObjects.Common.Models;
using Game.InteractableObjects.Common.Views;
using Game.InteractableObjects.Highlight.Models;
using Game.InteractableObjects.Highlight.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Interaction.Controllers
{
    public class CharacterObjectInteractionController : ITickable
    {
        [Inject] private CharacterWeaponPickUpModel CharacterWeaponPickUpModel { get; }
        [Inject] private CameraModel CameraModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CharacterObjectInteractionModel CharacterObjectInteractionModel { get; }

        private SelectionOutlineController _selectionOutlineController;
        
        // TODO: temp solution: refactor after highlight objects system is reworked (has to be injected)
        private SelectionOutlineController SelectionOutlineController => 
            _selectionOutlineController ??= GameObject.FindObjectOfType<SelectionOutlineController>();
        
        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;
        
        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var interactableObjectView = CheckForObjectsToInteract();
            bool foundInteractableObject = interactableObjectView != null;
            if (!foundInteractableObject)
                return;

            bool pickUpWeaponInput = InputModel.PickUpWeaponButtonClickInput;
            if (!pickUpWeaponInput)
                return;
            
            HandleInteractableObject(interactableObjectView);
        }

        private InteractableObjectView CheckForObjectsToInteract()
        {
            Vector3 origin = MainCameraTransform.position;
            Vector3 direction = MainCameraTransform.forward;
            float rayDistance = CharacterObjectInteractionModel.MaxPickUpDistance;

            if (Physics.Raycast(origin, direction, out var raycastHit, rayDistance))
            {
                if (raycastHit.collider.TryGetComponent<HighlightableObjectView>(out var highlightableObjectView))
                {
                    var renderer = highlightableObjectView.Context.Container.Resolve<HighlightableObjectModel>().GetRenderer();
                    SelectionOutlineController.UpdateTargetRenderer(renderer);
                }
                else
                {
                    SelectionOutlineController.UpdateTargetRenderer(null);
                }

                if (raycastHit.collider.TryGetComponent<InteractableObjectView>(out var interactableObjectView))
                {
                    CharacterObjectInteractionModel.DetectInteractableObject(interactableObjectView);
                    return interactableObjectView;
                }
            }
            else
            {
                SelectionOutlineController.UpdateTargetRenderer(null);
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
                    var equippedWeapon = new EquippedWeapon(container);
                    CharacterWeaponPickUpModel.PickUp(equippedWeapon);
                    break;
                
                default:
                    Debug.LogError($"Unknown interactable object type! No handler for {objectType}");
                    break;
            }
        }
    }
}
