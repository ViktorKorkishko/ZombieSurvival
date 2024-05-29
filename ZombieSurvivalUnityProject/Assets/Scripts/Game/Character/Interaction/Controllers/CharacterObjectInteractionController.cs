using Core.Exceptions;
using Game.Character.ObjectDetector.Models;
using Game.Inputs.Models;
using Game.InteractableObjects.Common.Enums;
using Game.InteractableObjects.Common.Models;
using Game.InteractableObjects.Common.Views;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using Game.Inventory.Core.Models;
using Game.Inventory.Items.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Interaction.Controllers
{
    public class CharacterObjectInteractionController : ITickable
    {
        [Inject] private ObjectDetectorModel ObjectDetectorModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private InventoryModel InventoryModel { get; }

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool pickUpWeaponInput = InputModel.InteractObjectButtonClickInput;
            if (!pickUpWeaponInput)
                return;

            var detectedObject = ObjectDetectorModel.CurrentlyDetectedObject;
            bool isObjectDetected = detectedObject != null;
            if (!isObjectDetected)
                return;

            HandleDetectedObject(detectedObject);
        }

        private void HandleDetectedObject(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent<InteractableObjectView>(out var interactableObjectView))
                return;
            
            HandleInteractableObject(interactableObjectView);
        }

        private void HandleInteractableObject(InteractableObjectView interactableObjectView)
        {
            var container = interactableObjectView.Context.Container;
            var model = container.TryResolve<InteractableObjectModel>();
            
            bool successfullyResolved = model != null;
            if (!successfullyResolved)
            {
                Debug.LogError($"Cannot resolve {typeof(InteractableObjectModel)}");
                return;
            }
            
            var objectType = model.Type;
            switch (objectType)
            {
                case InteractableObjectType.InteractableObject:
                    model.Interact();
                    break;
                
                case InteractableObjectType.PickableItem:
                    var pickableItem = container.Resolve<PickableItemModel>();
                    var itemData = pickableItem.PickUp();
                    var inventoryItemModel = new InventoryItemModel(itemData);
                    InventoryModel.AddItems(new[] { inventoryItemModel });
                    break;
                
                default:
                    Debug.LogException(new EnumNotSupportedException<InteractableObjectType>(objectType));
                    break;
            }
        }
    }
}
