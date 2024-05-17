using System;
using Game.ItemsDB.Item.Enums;

namespace Game.Inventory.Items.Models
{
    public class InventoryItemModel : ICloneable
    {
        public InventoryBaseItemData Data { get; private set; }
        
        public InventoryItemModel(ItemId id, int count)
        {
            Data = new InventoryBaseItemData()
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
