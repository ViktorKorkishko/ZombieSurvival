using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;

namespace Game.Inventory.DragAndDrop.Models
{
    public class DragAndDropModel
    {
        public Action<CellView, CellModel> OnCellRegistered { get; set; }
        public Action<CellView> OnCellUnregistered { get; set; }
        
        private Dictionary<CellView, CellModel> _cellViewToModelMap { get; } = new();

        public void RegisterDraggableCells(CellsContainerModel cellsContainerModel)
        {
            var cells = cellsContainerModel.CellContainers.Select(x => (x.View, x.Model));
            foreach (var cell in cells)
            {
                if (_cellViewToModelMap.TryAdd(cell.View, cell.Model))
                {
                    OnCellRegistered?.Invoke(cell.View, cell.Model);
                }
            }
        }

        public void UnregisterDraggableCells(CellsContainerModel cellsContainerModel)
        {
            var cellsViews = cellsContainerModel.CellContainers.Select(x => x.View);
            foreach (var cellView in cellsViews)
            {
                _cellViewToModelMap.Remove(cellView);
                OnCellUnregistered?.Invoke(cellView);
            }
        }
    }
}
