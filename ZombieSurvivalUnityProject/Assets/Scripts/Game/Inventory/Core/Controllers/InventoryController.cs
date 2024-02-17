using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.Items.Models;
using Game.ItemsDB;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryController : IInitializable, IDisposable
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private InventoryView InventoryView { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }

        private readonly List<CellContainer> _cellsContainers = new();

        void IInitializable.Initialize()
        {
            InventoryModel.OnItemsAdded += HandleOnItemsAdded;

            Init();
        }

        void IDisposable.Dispose()
        {
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;
        }

        void Init()
        {
            var inventoryCellCount = InventoryModel.InventoryCellsCount;
            for (int i = 0; i < inventoryCellCount; i++)
            {
                var cellModel = new CellModel();
                var cellView = InventoryView.InitCell(cellModel);
                var cellController = new CellController(cellModel, cellView);

                cellController.Init();

                _cellsContainers.Add(new CellContainer(cellModel, cellView, cellController));
            }
        }

        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            Debug.Log("Items being added!");
            SpreadItemsAmongCells(items);
        }

        private void SpreadItemsAmongCells(IEnumerable<InventoryItemModel> iitems)
        {
            var cellModels = _cellsContainers.Select(x => x.Model).ToList();

            for (int i = 0; i < cellModels.Count; i++)
            {
                var currentCell = cellModels[i];
                
                var items = iitems.ToList();
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
                                    int itemsToAddCount = currentItem.Count >= itemsCountCanBeAdded ? itemsCountCanBeAdded : currentItem.Count;   
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
                            int itemsToAddCount = currentItem.Count >= itemsCountCanBeAdded ? itemsCountCanBeAdded : currentItem.Count;   
                            var newCellItem = new InventoryItemModel(itemId, itemsToAddCount, ItemsDataBase);
                            currentCell.SetItem(newCellItem);
                            currentItem.Count -= itemsToAddCount;
                        }
                    }

                    if (AllItemsSpread(items))
                    {
                        return;
                    }
                }
            }
        }

        private bool AllItemsSpread(IEnumerable<InventoryItemModel> items)
        {
            return items.All(x => x.Count <= 0);
        }
    }
}