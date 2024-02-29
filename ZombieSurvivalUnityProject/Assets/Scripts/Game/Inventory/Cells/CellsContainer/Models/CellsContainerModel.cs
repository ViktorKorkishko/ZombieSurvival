using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Items.Enums;
using Game.Inventory.Items.Models;
using Game.ItemsDB;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Cells.CellsContainer.Models
{
    public class CellsContainerModel
    {
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private CellController.Factory CellControllerFactory { get; }
        
        public bool IsInited { get; private set; }

        public Action OnInitialized { get; set; }

        public IEnumerable<CellModel> Cells => CellContainers.Select(x => x.Model);
        public List<CellContainer> CellContainers { get; } = new();
        
        public Func<CellView> OnCellViewInitialized { get; set; }
        
        public void InitCells(IEnumerable<CellModel.Data> cellsData)
        {
            var cellsDataList = cellsData.ToList();
            for (int i = 0; i < cellsDataList.Count; i++)
            {
                var cellModel = new CellModel();
                var cellView = OnCellViewInitialized?.Invoke();
                var cellController = CellControllerFactory.Create(cellModel, cellView);
                cellController.Init();

                var cellData = cellsDataList[i];
                if (cellData.ItemId != ItemId.None)
                {
                    var inventoryItemModel = new InventoryItemModel(cellData.ItemId, cellData.Count);
                    cellModel.SetItem(inventoryItemModel);
                }
                else
                {
                    cellModel.RemoveItem();
                }

                CellContainers.Add(new CellContainer(cellModel, cellView, cellController));
            }

            IsInited = true;
            OnInitialized?.Invoke();
        }
        
        public void SpreadItemsAmongCells(IEnumerable<InventoryItemModel> iitems)
        {
            var cells = CellContainers.Select(x => x.Model).ToList();
            var items = iitems.ToList();

            for (int i = 0; i < cells.Count; i++)
            {
                var currentCell = cells[i];
                for (int j = 0; j < items.Count; j++)
                {
                    var currentItem = items[j];
                    if (currentCell.ContainsItem)
                    {
                        var itemId = currentCell.ItemId;
                        bool containsSameItem = itemId == currentItem.ItemId;
                        if (containsSameItem)
                        {
                            if (ItemsDataBase.TryGetItemData(itemId, out var itemData))
                            {
                                bool fullyStacked = currentCell.ItemCount == itemData.MaxStackCount;
                                if (!fullyStacked)
                                {
                                    int itemsCountCanBeAdded = itemData.MaxStackCount - currentCell.ItemCount;
                                    int itemsToAddCount = currentItem.Count >= itemsCountCanBeAdded
                                        ? itemsCountCanBeAdded
                                        : currentItem.Count;
                                    if (itemsCountCanBeAdded > 0)
                                    {
                                        currentCell.AdjustItemCount(itemsToAddCount);
                                        currentItem.Count -= itemsToAddCount;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var itemId = currentItem.ItemId;
                        if (ItemsDataBase.TryGetItemData(itemId, out var itemData))
                        {
                            int itemsCountCanBeAdded = itemData.MaxStackCount - 0;
                            int itemsToAddCount = currentItem.Count >= itemsCountCanBeAdded
                                ? itemsCountCanBeAdded
                                : currentItem.Count;
                            var newCellItem = new InventoryItemModel(itemId, itemsToAddCount);
                            currentCell.SetItem(newCellItem);
                            currentItem.Count -= itemsToAddCount;
                        }
                    }

                    if (AllItemsAreSpread(items))
                    {
                        return;
                    }
                }
            }
            
            // TODO: handle cases when all of the cells are iterated and there are still items that cannot fit
            if (!AllItemsAreSpread(items))
            {
                Debug.Log("UNSPREAD ITEMS ARE LEFT!");
            }
        }
        
        private bool AllItemsAreSpread(IEnumerable<InventoryItemModel> items)
        {
            return items.All(x => x.Count <= 0);
        }
    }
}
