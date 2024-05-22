using System;
using Game.Items.Data;
using Game.Items.Enums;

namespace Game.Inventory.Items.Models
{
    public class InventoryItemModel : ICloneable
    {
        public ItemData Data { get; private set; }
        
        public InventoryItemModel(ItemId id, int count)
        {
            Data = new ItemData
            {
                Id = id,
                Count = count,
            };
        }
        
        public object Clone()
        {
            var clone = MemberwiseClone();
            return clone;
        }
    }
}
