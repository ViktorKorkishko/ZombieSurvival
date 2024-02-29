using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;

namespace Game.Inventory.Cells
{
    public class SelectionController : IDisposable
    {
        public CellModel CurrentlySelectedCell { get; private set; }

        public Action<CellModel> OnSelectedCellChanged { get; set; }

        private readonly List<CellModel> _cells;

        public SelectionController(IEnumerable<CellModel> cellModels)
        {
            _cells = cellModels.ToList();
        }
        
        public void Initialize()
        {
            _cells.ForEach(x =>
            {
                x.OnSelected += HandleOnCellSelected;
                x.SetSelected(false);
            });
            
            _cells[0].SetSelected(true);
        }

        void IDisposable.Dispose()
        {
            _cells.ForEach(x =>
            {
                x.OnSelected -= HandleOnCellSelected;
            });
        }

        private void HandleOnCellSelected(CellModel cellModel, bool selected)
        {
            if (!selected)
                return;
            
            if (cellModel == CurrentlySelectedCell)
                return;
            
            CurrentlySelectedCell?.SetSelected(false);
            CurrentlySelectedCell = cellModel;

            OnSelectedCellChanged?.Invoke(CurrentlySelectedCell);
        }
    }
}
