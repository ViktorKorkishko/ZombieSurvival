using System;
using Game.Inventory.Items.Enums;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel
    {
        public bool IsSelected { get; private set; }
        
        public bool ContainsItem => InventoryItemModel != null;
        public int ItemCount => InventoryItemModel.Count;
        public ItemId ItemId => InventoryItemModel.ItemId;
        
        public Action<CellModel, bool> OnSelected { get; set; }
        public Action<InventoryItemModel> OnItemSet { get; set; }
        public Action<CellModel, InventoryItemModel> OnItemSetToCell { get; set; }
        public Action OnItemRemoved { get; set; }
        public Action<CellModel> OnItemRemovedFromCell { get; set; }
        public Action<int> OnItemCountChanged { get; set; }
        
        private InventoryItemModel InventoryItemModel { get; set; }

        public void SetItem(InventoryItemModel itemModel)
        {
            InventoryItemModel = itemModel;
            OnItemSet?.Invoke(InventoryItemModel);
            OnItemSetToCell?.Invoke(this, InventoryItemModel);
        }

        public InventoryItemModel RemoveItem()
        {
            var returnItem = (InventoryItemModel)InventoryItemModel?.Clone();
            InventoryItemModel = null;
            OnItemRemoved?.Invoke();
            OnItemRemovedFromCell?.Invoke(this);
            return returnItem;
        }

        public void AdjustItemCount(int count)
        {
            InventoryItemModel.Count += count;

            if (InventoryItemModel.Count == 0)
            {
                RemoveItem();
            }
            else
            {
                OnItemCountChanged?.Invoke(InventoryItemModel.Count);
            }
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            OnSelected?.Invoke(this, selected);
        }
    }
}
