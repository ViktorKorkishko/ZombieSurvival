using System;
using System.Collections.Generic;
using System.Linq;
using Game.Common.SelectableCollection.Interfaces;

namespace Game.Common.SelectableCollection
{
    public class SelectableCollection<T> : IDisposable
        where T : class, ISelectable
    {
        public T SelectedElement { get; private set; }
        
        public event Action<T> OnSelectedChanged;
        
        private IEnumerable<T> _selectables;
        
        public SelectableCollection(IEnumerable<T> selectables)
        {
            _selectables = selectables;
        }
        
        public void Initialize(bool selectFirst = true)
        {
            if (!_selectables.Any())
                throw new ArgumentException("Collection is empty");
            
            if (_selectables.Any(x => x == null))
                throw new ArgumentException("Collection contains null element");
            
            foreach (var element in _selectables)
            {
                element.OnSelected += HandleOnCellSelected;
                element.SetSelected(false);
            }
            
            _selectables.ElementAt(0).SetSelected(selectFirst);
        }
        
        void IDisposable.Dispose()
        {
            foreach (var element in _selectables)
            {
                element.OnSelected -= HandleOnCellSelected;
            }
            
            _selectables = Enumerable.Empty<T>();
        }
        
        private void HandleOnCellSelected(ISelectable selectable, bool selected)
        {
            if (!selected)
                return;
            
            if (selectable == SelectedElement)
                return;
            
            SelectedElement?.SetSelected(false);
            SelectedElement = selectable as T;
            
            OnSelectedChanged?.Invoke(SelectedElement);
        }
    }
}