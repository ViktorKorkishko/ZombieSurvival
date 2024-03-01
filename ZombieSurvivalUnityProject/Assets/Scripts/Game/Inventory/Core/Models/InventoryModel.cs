using System;
using System.Collections.Generic;
using Core.Installers;
using Core.SaveSystem.Models;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.HotBar.Models;
using Game.Inventory.Items.Models;
using Zenject;

namespace Game.Inventory.Core.Models
{
    public partial class InventoryModel : SaveableModel<InventoryModel.Data>
    {
        [Inject(Id = BindingIdentifiers.InventoryCellsContainer)] public CellsContainerModel InventoryCellsContainerModel { get; }
        [Inject(Id = BindingIdentifiers.InventoryHotBarCellsContainer)] public CellsContainerModel InventoryHotBarCellsContainer { get; }
        [Inject] private HotBarModel HotBarModel { get; }
        
        public int InitialInventoryCellsCount { get; }

        public Action<IEnumerable<InventoryItemModel>> OnItemsAdded { get; set; }
        
        public InventoryModel(int initialInventoryCellsCount)
        {
            InitialInventoryCellsCount = initialInventoryCellsCount;
        }

        public void InitializeCells()
        {
            InventoryCellsContainerModel.InitCells(base.Data.InventoryCellsData);
            InventoryHotBarCellsContainer.InitCells(base.Data.HotBarCellsData);
        }

        public void AddItems(IEnumerable<InventoryItemModel> items)
        {
            OnItemsAdded?.Invoke(items);
        }
    }
}
