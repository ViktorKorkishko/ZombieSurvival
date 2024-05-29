using Core.Installers;
using Game.InteractableObjects.Common.Controllers;
using Game.InteractableObjects.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.PickableItem.Controllers
{
    public class PickableItemController : InteractableObjectControllerBase
    {
        [Inject] private InteractableObjectView InteractableObjectView { get; }
        [Inject(Id = BindingIdentifiers.Root)] private Transform RootTransform { get; }
        
        protected override void HandleOnObjectInteract()
        {
            RootTransform.gameObject.SetActive(false);
            Debug.Log($"Interacted with {InteractableObjectView.name}");
        }
    }
}
