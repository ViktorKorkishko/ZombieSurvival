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

        private int _cellViewCount;
        
        public CellView InitCell()
        {
            var newCellView = OnCellViewCreated?.Invoke();
            if (newCellView != null)
            {
                newCellView.transform.SetParent(_cellsParentTransform);
                newCellView.name += $"_{_cellViewCount++}";
                return newCellView;
            }

            return null;
        }
    }
}
