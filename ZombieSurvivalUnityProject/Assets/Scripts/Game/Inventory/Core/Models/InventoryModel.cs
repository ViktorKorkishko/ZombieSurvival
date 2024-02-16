using System;
using System.Collections.Generic;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Core.Models
{
    public class InventoryModel
    {
        public int InventoryCellsCount { get; }

        public Action<IEnumerable<InventoryItemModel>> OnItemsAdded { get; set; }
        
        public InventoryModel(int inventoryCellsCount)
        {
            InventoryCellsCount = inventoryCellsCount;
        }

        public void AddItems(IEnumerable<InventoryItemModel> items)
        {
            OnItemsAdded?.Invoke(items);
        }
    }
}
