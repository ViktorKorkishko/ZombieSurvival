﻿using System;
using System.Runtime.Serialization;
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
        public Action OnItemRemoved { get; set; }
        public Action<int> OnItemCountChanged { get; set; }
        
        private InventoryItemModel InventoryItemModel { get; set; }
        
        public Data GetSaveData()
        {
            return new Data
            {
                ItemId = ItemId,
                Count = ItemCount,
            };
        }

        public void SetItem(InventoryItemModel itemModel)
        {
            InventoryItemModel = itemModel;
            OnItemSet?.Invoke(InventoryItemModel);
        }

        public InventoryItemModel RemoveItem()
        {
            var returnItem = (InventoryItemModel)InventoryItemModel?.Clone();
            InventoryItemModel = null;
            OnItemRemoved?.Invoke();
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
