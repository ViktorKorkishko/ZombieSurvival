using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.Items.Models;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryController : IInitializable, IDisposable
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private InventoryView InventoryView { get; }
        
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
            Debug.Log("Items added!");
            var cell = _cellsContainers[0].Model;
            var item = items.ElementAt(0);
            cell.SetItem(item);
        }
    }
}