using System;
using Game.Inventory.Cells.Core.Views;
using UnityEngine;

namespace Game.Inventory.Cells.CellsContainer.Views
{
    public class CellsContainerView : MonoBehaviour
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