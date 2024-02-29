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
        private CellsContainerModel FirstCellsContainer { get; }
        private CellsContainerModel SecondCellsContainer { get; }

        private List<CellModel> FirstCells => FirstCellsContainer.Cells.ToList();
        private List<CellModel> SecondCells => SecondCellsContainer.Cells.ToList();
        
        public SynchronizableCellsContainer(CellsContainerModel firstCellsContainer, CellsContainerModel secondCellsContainer)
        {
            FirstCellsContainer = firstCellsContainer;
            SecondCellsContainer = secondCellsContainer;
        }

        public void Initialize()
        {
            if (FirstCellsContainer.IsInited)
            {
                Initialize_Internal();
            }
            else
            {
                FirstCellsContainer.OnInitialized += Initialize_Internal;
            }
        }

        private void Initialize_Internal()
        {
            var cellsData = FirstCells.Select(x => x.GetSaveData());
            SecondCellsContainer.InitCells(cellsData);
            
            foreach (var cell in FirstCells)
            {
                cell.OnItemSetToCell += HandleOnItemSetToFirstCellsContainer;
                cell.OnItemRemovedFromCell += HandleOnItemRemovedFromFirstCellsContainer;
            }
            
            foreach (var cell in SecondCells)
            {
                cell.OnItemSetToCell += HandleOnItemSetToSecondCellsContainer;
            }
        }

        void IDisposable.Dispose()
        {
            foreach (var cell in FirstCells)
            {
                cell.OnItemSetToCell -= HandleOnItemSetToFirstCellsContainer;
            }
            
            foreach (var cell in SecondCells)
            {
                cell.OnItemSetToCell -= HandleOnItemSetToSecondCellsContainer;
            }
        }

        private void HandleOnItemSetToFirstCellsContainer(CellModel cellModel, InventoryItemModel inventoryItemModel)
        {
            var firstCells = FirstCellsContainer.Cells.ToList();
            int index = firstCells.IndexOf(cellModel);

            var secondCells = SecondCellsContainer.Cells.ToList();
            var cell = secondCells[index];

            cell.OnItemSetToCell -= HandleOnItemSetToSecondCellsContainer;
            cell.SetItem(inventoryItemModel);
            cell.OnItemSetToCell += HandleOnItemSetToSecondCellsContainer;
        }
        
        private void HandleOnItemSetToSecondCellsContainer(CellModel cellModel, InventoryItemModel inventoryItemModel)
        {
            var secondCells = SecondCellsContainer.Cells.ToList();
            int index = secondCells.IndexOf(cellModel);

            var firstCells = FirstCellsContainer.Cells.ToList();
            var cell = firstCells[index];

            cell.OnItemSetToCell -= HandleOnItemSetToFirstCellsContainer;
            cell.SetItem(inventoryItemModel);
            cell.OnItemSetToCell += HandleOnItemSetToFirstCellsContainer;
        }

        private void HandleOnItemRemovedFromFirstCellsContainer(CellModel cellModel)
        {
            var firstCells = FirstCellsContainer.Cells.ToList();
            int index = firstCells.IndexOf(cellModel);

            var secondCells = SecondCellsContainer.Cells.ToList();
            var cell = secondCells[index];

            cell.OnItemRemovedFromCell -= HandleOnItemRemovedFromSecondCellsContainer;
            cell.RemoveItem();
            cell.OnItemRemovedFromCell += HandleOnItemRemovedFromSecondCellsContainer;
        }
        
        private void HandleOnItemRemovedFromSecondCellsContainer(CellModel cellModel)
        {
            var secondCells = SecondCellsContainer.Cells.ToList();
            int index = secondCells.IndexOf(cellModel);

            var firstCells = FirstCellsContainer.Cells.ToList();
            var cell = firstCells[index];

            cell.OnItemRemovedFromCell -= HandleOnItemRemovedFromFirstCellsContainer;
            cell.RemoveItem();
            cell.OnItemRemovedFromCell += HandleOnItemRemovedFromFirstCellsContainer;
        }
    }
}