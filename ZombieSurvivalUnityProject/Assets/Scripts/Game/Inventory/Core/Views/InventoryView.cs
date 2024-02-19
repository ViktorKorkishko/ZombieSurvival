using System;
using Core.ViewSystem.Views;
using Game.Inventory.Cells.Core.Views;
using UnityEngine;

namespace Game.Inventory.Core.Views
{
    public class InventoryView : ViewBase
    {
        [SerializeField] private Transform _cellsParentTransform;
            
        public Func<CellView> OnCellViewCreated { get; set; }

        public CellView InitCell()
        {
            var newCellView = OnCellViewCreated?.Invoke();
            if (newCellView != null)
            {
                newCellView.transform.SetParent(_cellsParentTransform);
                return newCellView;
            }

            return null;
        }
    }
}
