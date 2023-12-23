using System;
using Game.InteractableObjects.Common.Models;
using Zenject;

namespace Game.InteractableObjects.Common.Controllers
{
    public abstract class InteractableObjectControllerBase : IInitializable, IDisposable
    {
        [Inject] private InteractableObjectModel Model { get; }
        
        public virtual void Initialize()
        {
            Model.OnInteract += HandleOnObjectInteract;
        }

        public virtual void Dispose()
        {
            Model.OnInteract -= HandleOnObjectInteract;
        }

        protected abstract void HandleOnObjectInteract();
    }
}
