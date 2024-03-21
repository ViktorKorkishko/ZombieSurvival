using System;
using Game.InteractableObjects.Common.Views;
using UnityEngine;

namespace Game.Character.Interaction.Models
{
    public class CharacterObjectInteractionModel : MonoBehaviour
    {
        public Action<InteractableObjectView> OnInteractableObjectDetected { get; set; }

        public void DetectInteractableObject(InteractableObjectView view)
        {
            OnInteractableObjectDetected?.Invoke(view);
        }
    }
}