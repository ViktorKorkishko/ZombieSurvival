using System;
using Game.Items.Data;

namespace Game.Inventory.Items.Models
{
    public class InventoryItemModel : ICloneable
    {
        public ItemData Data { get; }
        
        public InventoryItemModel(ItemData itemData)
        {
            Data = itemData;
        }

        public object Clone()
        {
            var clonedData = (ItemData)Data.Clone();
            var itemModelClone = new InventoryItemModel(clonedData);
            return itemModelClone;
        }
    }
}
