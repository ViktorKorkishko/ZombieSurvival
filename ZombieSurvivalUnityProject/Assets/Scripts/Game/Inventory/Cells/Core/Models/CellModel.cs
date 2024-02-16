using System;
using Core.SaveSystem.Models;
using Game.Inventory.Items.Enums.Models;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel : SaveableModel<CellModel.Data>
    {
        public InventoryItemModel InventoryItemModel { get; private set; }

        public Action<InventoryItemModel> OnItemSet { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            SetupCellView();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            
        }

        private void SetupCellView()
        {
            
        }

        public void SetItem(InventoryItemModel itemModel)
        {
            InventoryItemModel = itemModel;
            
            OnItemSet?.Invoke(InventoryItemModel);
        }
    }
}
