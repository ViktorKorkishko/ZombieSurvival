using System;

namespace Game.Inventory.Cells.Interfaces
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        
        Action<ISelectable, bool> OnSelected { get; set; }

        void SetSelected(bool selected);
    }
}