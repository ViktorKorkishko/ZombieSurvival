using Core.ViewSystem.Views;
using Game.Inventory.Cells.CellsContainer.Views;
using UnityEngine;

namespace Game.Inventory.HotBar.Views
{
    public class HotBarView : ViewBase
    {
        [SerializeField] private CellsContainerView _cellsContainerView;

        public CellsContainerView CellsContainerView => _cellsContainerView;
    }
}
