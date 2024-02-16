using System;
using Core.SaveSystem.Models;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel : SaveableModel<CellModel.Data>
    {
        public InventoryItemModel InventoryItemModel { get; private set; }

        public Action<InventoryItemModel> OnItemSet { get; set; }
        
        public void SetItem(InventoryItemModel itemModel)
        {
            InventoryItemModel = itemModel;
            
            OnItemSet?.Invoke(InventoryItemModel);
        }
    }
}
