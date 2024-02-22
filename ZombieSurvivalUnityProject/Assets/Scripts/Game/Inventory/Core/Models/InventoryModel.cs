using System;
using System.Collections.Generic;
using Core.SaveSystem.Models;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Core.Models
{
    public partial class InventoryModel : SaveableModel<InventoryModel.Data>
    {
        public int InventoryCellsCount { get; }
        public List<CellModel.Data> Items { get; set; } = new();
        
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
