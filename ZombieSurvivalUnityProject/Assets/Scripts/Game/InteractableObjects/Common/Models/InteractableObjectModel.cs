using System;
using Game.InteractableObjects.Common.Enums;
using Zenject;

namespace Game.InteractableObjects.Common.Models
{
    public class InteractableObjectModel
    {
        [Inject] public InteractableObjectType Type { get; }

        public Action OnInteract { get; set; }

        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}
