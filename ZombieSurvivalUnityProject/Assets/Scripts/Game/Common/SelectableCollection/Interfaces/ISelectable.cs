using System;

namespace Game.Common.SelectableCollection.Interfaces
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        
        Action<ISelectable, bool> OnSelected { get; set; }

        void SetSelected(bool selected);
    }
}
