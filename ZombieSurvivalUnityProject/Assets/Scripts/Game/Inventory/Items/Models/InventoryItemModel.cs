using System;
using Game.Inventory.Items.Enums;

namespace Game.Inventory.Items.Models
{
    public class InventoryItemModel : ICloneable
    {
        public ItemId ItemId { get; }
        public int Count { get; set; }
        
        public InventoryItemModel(ItemId itemId, int count)
        {
            ItemId = itemId;
            Count = count;
        }

        public object Clone()
        {
            var clone = MemberwiseClone();
            return clone;
        }
    }
}
