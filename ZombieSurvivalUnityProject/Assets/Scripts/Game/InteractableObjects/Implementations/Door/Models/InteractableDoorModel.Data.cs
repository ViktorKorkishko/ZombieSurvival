using Game.InteractableObjects.Implementations.Door.Enums;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.Door.Models
{
    public partial class InteractableDoorModel
    {
        public new class Data
        {
            public DoorState DoorState { get; set; }
            public Quaternion Rotation { get; set; }
        }

        protected override string DataKey => "InteractableDoorModel.Data";
    }
}
