using Core.Installers;
using Core.SaveSystem.Saving.Common.Load;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Implementations.Door.Models
{
    public partial class InteractableDoorModel
    {
        [Inject(Id = BindingIdentifiers.Rigidbody)] private Rigidbody Rigidbody { get; }

        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            State = base.Data.DoorState;
            Rigidbody.transform.localRotation = base.Data.Rotation;
        }

        protected override void HandleOnDataPreSaved()
        {
            base.Data.DoorState = State;
            base.Data.Rotation = Rigidbody.transform.localRotation;
        }
    }
}
