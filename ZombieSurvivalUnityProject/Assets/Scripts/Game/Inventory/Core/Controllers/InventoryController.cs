using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.DragAndDrop.Models;
using Game.Inventory.Items.Models;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryController : IInitializable, IDisposable
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private DragAndDropModel DragAndDropModel { get; }

        private InventoryView InventoryView { get; }

        private CellModel _currentlySelectedCell;

        public InventoryController(InventoryView inventoryView)
        {
            InventoryView = inventoryView;
        }

        void IInitializable.Initialize()
        {
            InventoryModel.OnItemsAdded += HandleOnItemsAdded;

            InventoryView.OnShow += HandleOnShow;
            InventoryView.OnHide += HandleOnHide;
            InventoryView.OnDeleteItemButtonClicked += HandleOnDeleteItemButtonClicked;
            
            InventoryModel.InitializeCells();
            
            InitSelectedCell();
        }

        void IDisposable.Dispose()
        {
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;

            InventoryView.OnShow -= HandleOnShow;
            InventoryView.OnHide -= HandleOnHide;
            InventoryView.OnDeleteItemButtonClicked -= HandleOnDeleteItemButtonClicked;

            foreach (var cell in InventoryModel.CellsContainerModel.Cells)
            {
                cell.OnSelected -= HandleOnCellSelected;
            }
        }

        private void InitSelectedCell()
        {
            var cells = InventoryModel.CellsContainerModel.Cells.ToList();
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                
                var firstCell = i == 0;
                if (firstCell)
                {
                    _currentlySelectedCell = cell;
                    _currentlySelectedCell.SetSelected(true);
                    _currentlySelectedCell.OnSelected += HandleOnCellSelected;
                    
                    InventoryView.SetDeleteButtonEnabled(_currentlySelectedCell.ContainsItem);
                }
                else
                {
                    cell.OnSelected += HandleOnCellSelected;
                    cell.SetSelected(false);
                }
            }
        }

        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            InventoryModel.CellsContainerModel.SpreadItemsAmongCells(items);
        }

        private void HandleOnShow()
        {
            DragAndDropModel.RegisterDraggableCells(InventoryModel.CellsContainerModel);
        }

        private void HandleOnHide()
        {
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.CellsContainerModel);
        }

        private void HandleOnDeleteItemButtonClicked()
        {
            if (!_currentlySelectedCell.ContainsItem)
                return;

            _currentlySelectedCell.RemoveItem();
            InventoryView.SetDeleteButtonEnabled(false);
        }

        private void HandleOnCellSelected(CellModel cellModel, bool selected)
        {
            if (!selected)
                return;
            
            if (cellModel == _currentlySelectedCell)
                return;
            
            _currentlySelectedCell.SetSelected(false);
            _currentlySelectedCell = cellModel;

            InventoryView.SetDeleteButtonEnabled(cellModel.ContainsItem);
        }
    }
}
