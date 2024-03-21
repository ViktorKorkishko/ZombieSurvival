using Game.Character.ObjectDetector.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
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
        [Inject] private CharacterWeaponPickUpModel CharacterWeaponPickUpModel { get; }
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
                Debug.LogError($"Cannot Resolve {typeof(InteractableObjectModel)}");
                return;
            }
            
            var objectType = model.Type;
            switch (objectType)
            {
                case InteractableObjectType.Common:
                    model.Interact();
                    break;

                // case InteractableObjectType.Weapon:
                //     var equippedWeapon = new EquippedWeapon(container);
                //     CharacterWeaponPickUpModel.PickUp(equippedWeapon);
                //     break;

                case InteractableObjectType.PickableItem:
                    var pickableItem = container.Resolve<PickableItemModel>();
                    var inventoryItemModel = new InventoryItemModel(pickableItem.ItemId, pickableItem.Count);
                    InventoryModel.AddItems(new[] { inventoryItemModel });
                    pickableItem.PickUp();
                    break;

                default:
                    Debug.LogError($"Unknown interactable object type! No handler for {objectType}");
                    break;
            }
        }
    }
}
