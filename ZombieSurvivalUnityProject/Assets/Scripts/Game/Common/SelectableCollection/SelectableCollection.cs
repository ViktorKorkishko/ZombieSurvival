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
        
        public Action<T> OnSelectedChanged { get; set; }
        
        private readonly IList<T> _innerCollection;

        public SelectableCollection(IEnumerable<T> cellModels)
        {
            _innerCollection = cellModels.ToList();
        }
        
        public void Initialize()
        {
            foreach (var element in _innerCollection)
            {
                element.OnSelected += HandleOnCellSelected;
                element.SetSelected(false);
            }
            
            _innerCollection[0].SetSelected(true);
        }
        
        void IDisposable.Dispose()
        {
            foreach (var element in _innerCollection)
            {
                element.OnSelected -= HandleOnCellSelected;
            }
            
            _innerCollection.Clear();
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
