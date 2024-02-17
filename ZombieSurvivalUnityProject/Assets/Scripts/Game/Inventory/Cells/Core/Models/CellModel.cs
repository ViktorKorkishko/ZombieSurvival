﻿using System;
using Core.SaveSystem.Models;
using Game.Inventory.Items.Enums;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel : SaveableModel<CellModel.Data>
    {
        public bool ContainsItem => InventoryItemModel != null;
        public int ItemCount => InventoryItemModel.Count;
        public ItemId ItemId => InventoryItemModel.ItemId;
        
        public Action<InventoryItemModel> OnItemSet { get; set; }
        public Action OnItemRemoved { get; set; }
        public Action<int> OnItemCountChanged { get; set; }
        
        private InventoryItemModel InventoryItemModel { get; set; }

        public void SetItem(InventoryItemModel itemModel)
        {
            InventoryItemModel = itemModel;
            OnItemSet?.Invoke(InventoryItemModel);
        }

        public void RemoveItem()
        {
            InventoryItemModel = null;
            OnItemRemoved?.Invoke();
        }

        public void AdjustItemCount(int count)
        {
            InventoryItemModel.Count += count;
            OnItemCountChanged?.Invoke(InventoryItemModel.Count);
        }
    }
}
