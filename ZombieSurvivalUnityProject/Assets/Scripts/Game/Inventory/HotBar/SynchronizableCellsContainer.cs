using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Items.Models;

namespace Game.Inventory.HotBar
{
    public class SynchronizableCellsContainer : IDisposable
    {
        private CellsContainerModel CoreContainer { get; }
        private CellsContainerModel SecondaryCellsContainer { get; }
        
        private List<CellModel> CoreCells => CoreContainer.Cells.ToList();
        private List<CellModel> SecondaryCells => SecondaryCellsContainer.Cells.ToList();
        
        public SynchronizableCellsContainer(CellsContainerModel coreContainer, CellsContainerModel secondaryCellsContainer)
        {
            CoreContainer = coreContainer;
            SecondaryCellsContainer = secondaryCellsContainer;
        }
        
        public void Initialize()
        {
            if (CoreContainer.IsInited)
            {
                Initialize_Internal();
            }
            else
            {
                CoreContainer.OnInitialized += Initialize_Internal;
            }
        }
        
        private void Initialize_Internal()
        {
            var cellsData = CoreCells.Select(x => x.GetSaveData());
            SecondaryCellsContainer.InitCells(cellsData);
            
            foreach (var cell in CoreCells)
            {
                cell.OnItemSet += HandleOnItemSetToCoreCellsContainer;
                cell.OnItemRemoved += HandleOnItemRemovedFromCoreCellsContainer;
            }
            
            foreach (var cell in SecondaryCells)
            {
                cell.OnItemSet += HandleOnItemSetToSecondaryCellsContainer;
            }
        }
        
        void IDisposable.Dispose()
        {
            foreach (var cell in CoreCells)
            {
                cell.OnItemSet -= HandleOnItemSetToCoreCellsContainer;
            }
            
            foreach (var cell in SecondaryCells)
            {
                cell.OnItemSet -= HandleOnItemSetToSecondaryCellsContainer;
            }
        }
        
        private void HandleOnItemSetToCoreCellsContainer(CellModel cellModel, InventoryItemModel inventoryItemModel)
        {
            var firstCells = CoreCells.ToList();
            int index = firstCells.IndexOf(cellModel);

            var secondCells = SecondaryCells.ToList();
            var cell = secondCells[index];

            cell.OnItemSet -= HandleOnItemSetToSecondaryCellsContainer;
            cell.SetItem(inventoryItemModel);
            cell.OnItemSet += HandleOnItemSetToSecondaryCellsContainer;
        }
        
        private void HandleOnItemSetToSecondaryCellsContainer(CellModel cellModel, InventoryItemModel inventoryItemModel)
        {
            var secondCells = SecondaryCells.ToList();
            int index = secondCells.IndexOf(cellModel);

            var firstCells = CoreCells.ToList();
            var cell = firstCells[index];

            cell.OnItemSet -= HandleOnItemSetToCoreCellsContainer;
            cell.SetItem(inventoryItemModel);
            cell.OnItemSet += HandleOnItemSetToCoreCellsContainer;
        }

        private void HandleOnItemRemovedFromCoreCellsContainer(CellModel cellModel)
        {
            var coreCells = CoreCells.ToList();
            int index = coreCells.IndexOf(cellModel);

            var secondaryCells = SecondaryCells.ToList();
            var cell = secondaryCells[index];

            cell.OnItemRemoved -= HandleOnItemRemovedFromSecondaryCellsContainer;
            cell.RemoveItem();
            cell.OnItemRemoved += HandleOnItemRemovedFromSecondaryCellsContainer;
        }
        
        private void HandleOnItemRemovedFromSecondaryCellsContainer(CellModel cellModel)
        {
            var secondaryCells = SecondaryCells.ToList();
            int index = secondaryCells.IndexOf(cellModel);

            var coreCells = CoreCells.ToList();
            var cell = coreCells[index];

            cell.OnItemRemoved -= HandleOnItemRemovedFromCoreCellsContainer;
            cell.RemoveItem();
            cell.OnItemRemoved += HandleOnItemRemovedFromCoreCellsContainer;
        }
    }
}