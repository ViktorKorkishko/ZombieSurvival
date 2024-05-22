using System;
using Game.Common.SelectableCollection.Interfaces;
using Game.Inventory.Items.Models;
using Game.Items.Enums;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel : ISelectable
    {
        public bool IsSelected { get; private set; }
        public bool ContainsItem => _item != null;
        
        public ItemId ItemId => _item.Data.Id;
        public int ItemCount => _item.Data.Count;
        
        public Action<CellModel, InventoryItemModel> OnItemSet { get; set; }
        public Action<CellModel> OnItemRemoved { get; set; }
        public Action<int> OnItemCountChanged { get; set; }
        public Action<ISelectable, bool> OnSelected { get; set; }

        private InventoryItemModel _item;

        public void SetItem(InventoryItemModel itemModel)
        {
            _item = itemModel;
            OnItemSet?.Invoke(this, _item);
        }

        public InventoryItemModel RemoveItem()
        {
            var returnItem = (InventoryItemModel)_item?.Clone();
            _item = null;
            OnItemRemoved?.Invoke(this);
            return returnItem;
        }

        public void AdjustItemCount(int count)
        {
            _item.Data.Count += count;

            if (_item.Data.Count == 0)
            {
                RemoveItem();
            }
            else
            {
                OnItemCountChanged?.Invoke(_item.Data.Count);
            }
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            OnSelected?.Invoke(this, selected);
        }
    }
}
