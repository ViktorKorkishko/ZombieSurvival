using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Interfaces;

namespace Game.Inventory.Cells
{
    public class CellSelectionService : IDisposable
    {
        public ISelectable CurrentlySelectedCell { get; private set; }

        public Action<CellModel> OnSelectedCellChanged { get; set; }

        private readonly List<CellModel> _cells;

        public CellSelectionService(IEnumerable<CellModel> cellModels)
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

        private void HandleOnCellSelected(ISelectable selectable, bool selected)
        {
            if (!selected)
                return;
            
            if (selectable == CurrentlySelectedCell)
                return;
            
            CurrentlySelectedCell?.SetSelected(false);
            CurrentlySelectedCell = selectable;

            OnSelectedCellChanged?.Invoke(CurrentlySelectedCell as CellModel);
        }
    }
}
