using System;
using Game.Inventory.Cells.Interfaces;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel : ISelectable
    {
        public InventoryItemModel Item { get; private set; }
        
        public bool IsSelected { get; private set; }
        
        public bool ContainsItem => Item != null;
        
        public Action<CellModel, InventoryItemModel> OnItemSet { get; set; }
        public Action<CellModel> OnItemRemoved { get; set; }
        public Action<int> OnItemCountChanged { get; set; }
        public Action<ISelectable, bool> OnSelected { get; set; }

        public void SetItem(InventoryItemModel itemModel)
        {
            Item = itemModel;
            OnItemSet?.Invoke(this, Item);
        }

        public InventoryItemModel RemoveItem()
        {
            var returnItem = (InventoryItemModel)Item?.Clone();
            Item = null;
            OnItemRemoved?.Invoke(this);
            return returnItem;
        }

        public void AdjustItemCount(int count)
        {
            Item.Data.Count += count;

            if (Item.Data.Count == 0)
            {
                RemoveItem();
            }
            else
            {
                OnItemCountChanged?.Invoke(Item.Data.Count);
            }
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            OnSelected?.Invoke(this, selected);
        }
    }
}
