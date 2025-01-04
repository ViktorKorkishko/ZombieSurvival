using System;

namespace Game.Common.SelectableCollection.Interfaces
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        
        event Action<ISelectable, bool> OnSelected;
        
        void SetSelected(bool selected);
    }
}
