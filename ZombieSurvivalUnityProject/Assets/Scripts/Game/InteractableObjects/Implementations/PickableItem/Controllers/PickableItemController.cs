using Core.Installers;
using Game.InteractableObjects.Common.Controllers;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.PickableItem.Controllers
{
    public class PickableItemController : InteractableObjectControllerBase
    {
        [Inject] private PickableItemModel PickableItemModel { get; }
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

        // TODO: remove later, change architecture
        protected override void HandleOnObjectInteract() { }

        private void HandleOnPickedUp()
        {
            RootTransform.gameObject.SetActive(false);
        }
    }
}
