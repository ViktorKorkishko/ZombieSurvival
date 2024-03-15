using System;
using Core.Exceptions;
using Core.SaveSystem.Models;
using DG.Tweening;
using Game.InteractableObjects.Implementations.Door.Enums;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.Door.Models
{
    public partial class InteractableDoorModel : MonoSaveableModel<InteractableDoorModel.Data>
    {
        [Header("Angles")]
        [SerializeField] private Vector3 _openedDoorAngle;
        [SerializeField] private Vector3 _closedDoorAngle;

        [Header("Open/Close settings")] 
        [SerializeField] private Ease _ease;
        [SerializeField] private float _openDuration;
        
        [Header("Current door state")]
        [SerializeField] private DoorState _doorState;

        [Header("Interaction")] 
        [SerializeField] private bool _canInterruptInteraction;
        
        public Vector3 OpenAngle => _openedDoorAngle;
        public Vector3 CloseAngle => _closedDoorAngle;
        public Ease Ease => _ease;
        public float OpenDuration => _openDuration;
        
        public DoorState State
        {
            get => _doorState;
            private set => _doorState = value;
        }

        public bool IsBeingInteracted => State == DoorState.Opening || 
                                         State == DoorState.Closing;
        
        public Action<DoorState, Action> OnInteractionStarted { get; set; }
        public Action<DoorState, Action> OnInteractionInterrupted { get; set; }

        public void Interact()
        {
            if (IsBeingInteracted) 
            {
                if (_canInterruptInteraction)
                {
                    OnInteractionInterrupted?.Invoke(State, EndInteraction);
                    StartInterruption();
                    return;
                }
            }
            
            OnInteractionStarted?.Invoke(State, EndInteraction);
            SwitchState();
        }

        private void EndInteraction()
        {
            SwitchState();
        }
        
        private void StartInterruption()
        {
            switch (State)
            {
                case DoorState.Opening:
                    State = DoorState.Closing;
                    break;
                
                case DoorState.Closing:
                    State = DoorState.Opening;
                    break;
            }
        }

        private void SwitchState()
        {
            switch (State)
            {
                case DoorState.Opened:
                    State = DoorState.Closing;
                    break;
                
                case DoorState.Closed:
                    State = DoorState.Opening;
                    break;
                
                case DoorState.Opening:
                    State = DoorState.Opened;
                    break;
                
                case DoorState.Closing:
                    State = DoorState.Closed;
                    break;
                
                default:
                    Debug.LogError(new EnumNotSupportedException<DoorState>(State));
                    break;
            }
        }
    }
}
