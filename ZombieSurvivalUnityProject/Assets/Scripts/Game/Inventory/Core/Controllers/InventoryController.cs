using System;
using System.Collections.Generic;
using System.Linq;
using Core.ViewSystem.Views.Interfaces;
using Game.Common.SelectableCollection;
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
        
        private IEnumerable<CellModel> Cells
        {
            get
            {
                var inventoryCells = InventoryModel.InventoryCellsContainerModel.Cells;
                var hotBarCells = InventoryModel.InventoryHotBarCellsContainer.Cells;
                var cells = inventoryCells.Union(hotBarCells);
                return cells;
            }
        }

        private SelectableCollection<CellModel> _cellsSelectableCollection;

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

            _cellsSelectableCollection = new SelectableCollection<CellModel>(Cells);
            _cellsSelectableCollection.OnSelectedCellChanged += HandleOnSelectedCellChanged;
            
            _cellsSelectableCollection.Initialize();
        }

        void IDisposable.Dispose()
        {
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;

            InventoryView.OnShow -= HandleOnShow;
            InventoryView.OnHide -= HandleOnHide;
            InventoryView.OnDeleteItemButtonClicked -= HandleOnDeleteItemButtonClicked;
            
            _cellsSelectableCollection.OnSelectedCellChanged -= HandleOnSelectedCellChanged;
        }

        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            InventoryModel.InventoryCellsContainerModel.SpreadItemsAmongCells(items);
        }

        private void HandleOnShow()
        {
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);
        }

        private void HandleOnHide(IView view)
        {
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);
        }
        
        private void HandleOnDeleteItemButtonClicked()
        {
            if (!_cellsSelectableCollection.CurrentlySelectedCell.ContainsItem)
                return;

            _cellsSelectableCollection.CurrentlySelectedCell.RemoveItem();
            InventoryView.SetDeleteButtonEnabled(false);
        }

        private void HandleOnSelectedCellChanged(CellModel cellModel)
        {
            InventoryView.SetDeleteButtonEnabled(cellModel.ContainsItem);
        }
    }
}
