using System;
using System.Collections.Generic;
using System.Linq;
using Game.Common.SelectableCollection.Interfaces;

namespace Game.Common.SelectableCollection
{
    public class SelectableCollection<T> : IDisposable
        where T : class, ISelectable
    {
        public T CurrentlySelectedCell { get; private set; }

        public Action<T> OnSelectedCellChanged { get; set; }

        private readonly List<T> _cells;

        public SelectableCollection(IEnumerable<T> cellModels)
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
            foreach (var cell in _cells)
            {
                cell.OnSelected -= HandleOnCellSelected;
            }
            
            _cells.Clear();
        }

        private void HandleOnCellSelected(ISelectable selectable, bool selected)
        {
            if (!selected)
                return;
            
            if (selectable == CurrentlySelectedCell)
                return;
            
            CurrentlySelectedCell?.SetSelected(false);
            CurrentlySelectedCell = selectable as T;

            OnSelectedCellChanged?.Invoke(CurrentlySelectedCell);
        }
    }
}
