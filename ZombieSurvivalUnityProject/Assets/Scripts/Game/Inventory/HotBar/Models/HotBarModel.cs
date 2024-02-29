using System;
using System.Collections.Generic;
using Core.Installers;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Items.Models;
using Zenject;

namespace Game.Inventory.HotBar.Models
{
    public class HotBarModel
    {
        [Inject(Id = BindingIdentifiers.HotBarCellsContainer)] public CellsContainerModel HotBarCellsContainerModel { get; }
        [Inject(Id = BindingIdentifiers.InventoryHotBarCellsContainer)] public CellsContainerModel InventoryHotBarCellsContainer { get; }
        
        public int InitialHotBarCellsCount { get; }

        public Action<IEnumerable<InventoryItemModel>> OnItemsAdded { get; set; }
        
        public HotBarModel(int initialHotBarCellsCount)
        {
            InitialHotBarCellsCount = initialHotBarCellsCount;
        }

        public void AddItems(IEnumerable<InventoryItemModel> items)
        {
            OnItemsAdded?.Invoke(items);
        }
    }
}