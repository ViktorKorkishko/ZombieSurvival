using Game.InteractableObjects.Common.Controllers;
using Game.InteractableObjects.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.Weapon.Controllers
{
    public class InteractableWeaponController : InteractableObjectControllerBase
    {
        [Inject] private InteractableObjectView InteractableObjectView { get; }
        
        protected override void HandleOnObjectInteract()
        {
            // disable collider and rigidbody
            Debug.Log($"Interacted with {InteractableObjectView.name}");
        }
    }
}
