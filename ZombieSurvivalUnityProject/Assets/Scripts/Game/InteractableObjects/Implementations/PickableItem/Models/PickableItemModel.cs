using System;
using Game.Inventory.Items.Enums;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.PickableItem.Models
{
    public class PickableItemModel : MonoBehaviour
    {
        [SerializeField] private ItemId _itemId;
        [SerializeField] private int _count;

        public ItemId ItemId => _itemId;
        public int Count => _count;
        
        public Action OnPickedUp { get; set; }

        public void PickUp()
        {
            OnPickedUp?.Invoke();
        }
    }
}
