using Core.Installers;
using Core.SaveSystem.Saving.Common.Load;
using Game.InteractableObjects.Implementations.Door.Enums;
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
            switch (State)
            {
                case DoorState.Opening:
                    base.Data.DoorState = DoorState.Opened;
                    base.Data.Rotation = Quaternion.Euler(OpenAngle);
                    break;
                
                case DoorState.Closing:
                    base.Data.DoorState = DoorState.Closed;
                    base.Data.Rotation = Quaternion.Euler(CloseAngle);
                    return;
                
                default:
                    base.Data.DoorState = State;
                    base.Data.Rotation = Rigidbody.transform.localRotation;
                    return;
            }
        }
    }
}
