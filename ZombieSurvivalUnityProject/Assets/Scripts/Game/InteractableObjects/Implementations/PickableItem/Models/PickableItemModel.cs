using System;
using Game.Items.Data;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.PickableItem.Models
{
    public class PickableItemModel : MonoBehaviour
    {
        [field:SerializeField] public ItemData Data { get; private set; }

        public Action OnPickedUp { get; set; }

        public void Initialize(ItemData data)
        {
            Data = data;
        }

        public ItemData PickUp()
        {
            OnPickedUp?.Invoke();
            return (ItemData)Data.Clone();
        }
    }
}
