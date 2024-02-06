using System;
using Core.SaveSystem.Models;
using Game.InteractableObjects.Implementations.Door.Enums;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.Door.Models
{
    public partial class InteractableDoorModel : MonoSaveableModel<InteractableDoorModel.Data>
    {
        [SerializeField] private Vector3 _openedDoorAngle;
        [SerializeField] private Vector3 _closedDoorAngle;
        [SerializeField] private DoorState _doorState;
        [SerializeField] private float _openTime;

        public Vector3 OpenAngle => _openedDoorAngle;
        public Vector3 CloseAngle => _closedDoorAngle;
        public float OpenTime => _openTime;
        
        public DoorState State
        {
            get => _doorState;
            private set => _doorState = value;
        }
        
        public Action<DoorState, Action> OnInteractionStarted { get; set; }
        public Action OnInteractionEnded { get; set; }

        public void Interact()
        {
            OnInteractionStarted?.Invoke(State, EndInteraction);

            OnInteractionEnded += HandleOnInteractionEnded;
        }

        private void EndInteraction()
        {
            OnInteractionEnded?.Invoke();
        }

        private void HandleOnInteractionEnded()
        {
            OnInteractionEnded -= HandleOnInteractionEnded;
            
            SwitchDoorState();
        }

        private void SwitchDoorState()
        {
            switch (State)
            {
                case DoorState.Opened:
                    State = DoorState.Closed;
                    break;
                
                case DoorState.Closed:
                    State = DoorState.Opened;
                    break;
            }
        }
    }
}
