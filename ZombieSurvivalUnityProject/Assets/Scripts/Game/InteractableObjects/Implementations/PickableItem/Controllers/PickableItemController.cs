using Core.Installers;
using Game.InteractableObjects.Common.Controllers;
using Game.InteractableObjects.Common.Views;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.PickableItem.Controllers
{
    public class PickableItemController : InteractableObjectControllerBase
    { 
        [Inject] private PickableItemModel PickableItemModel { get; }
        [Inject] private InteractableObjectView InteractableObjectView { get; }
        [Inject(Id = BindingIdentifiers.Root)] private Transform RootTransform { get; }

        public override void Initialize()
        {
            base.Initialize();

            PickableItemModel.OnPickedUp += HandleOnPickedUp;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            PickableItemModel.OnPickedUp -= HandleOnPickedUp;
        }

        private void HandleOnPickedUp()
        {
            RootTransform.gameObject.SetActive(false);
        }

        protected override void HandleOnObjectInteract()
        {
            // disable collider and rigidbody
            PickableItemModel.PickUp();
            Debug.Log($"Interacted with {InteractableObjectView.name}");
        }
    }
}
