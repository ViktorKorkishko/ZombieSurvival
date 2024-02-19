using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.DragAndDrop.Controllers;
using Game.Inventory.Items.Models;
using Game.ItemsDB;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryController : IInitializable, IDisposable
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private CellView.Factory CellViewFactory { get; }

        private InventoryView InventoryView { get; }

        private DragAndDropController DragAndDropController { get; set; }

        private readonly List<CellContainer> _cellsContainers = new();

        public InventoryController(InventoryView inventoryView)
        {
            InventoryView = inventoryView;
        }

        void IInitializable.Initialize()
        {
            InventoryView.OnCellViewCreated += HandleOnCellViewCreated;
            InventoryModel.OnItemsAdded += HandleOnItemsAdded;

            InitCells();

            DragAndDropController = new DragAndDropController(InventoryView.transform, _cellsContainers, ItemsDataBase);
            DragAndDropController.Init();
            
            void InitCells()
            {
                var inventoryCellCount = InventoryModel.InventoryCellsCount;
                for (int i = 0; i < inventoryCellCount; i++)
                {
                    var cellModel = new CellModel();
                    var cellView = InventoryView.InitCell();
                    var cellController = new CellController(cellModel, cellView);

                    cellController.Init();

                    _cellsContainers.Add(new CellContainer(cellModel, cellView, cellController));
                }
            }
        }

        void IDisposable.Dispose()
        {
            InventoryView.OnCellViewCreated -= HandleOnCellViewCreated;
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;
        }

        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            SpreadItemsAmongCells(items);
        }

        // TODO: refactor (extract same parts to separate methods)
        private void SpreadItemsAmongCells(IEnumerable<InventoryItemModel> iitems)
        {
            var cells = _cellsContainers.Select(x => x.Model).ToList();
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
                            var newCellItem = new InventoryItemModel(itemId, itemsToAddCount, ItemsDataBase);
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
                string unspreadItems = "";
                items.Select(x => x).Where(x => x.Count > 0).ToList().ForEach(x =>
                {
                    unspreadItems += $"[ItemId: {x.ItemId}], Count: {x.Count}\n";
                });

                Debug.Log(unspreadItems);
            }
        }

        private bool AllItemsAreSpread(IEnumerable<InventoryItemModel> items)
        {
            return items.All(x => x.Count <= 0);
        }

        private CellView HandleOnCellViewCreated()
        {
            var cellView = CellViewFactory.Create();
            return cellView;
        }
    }
}