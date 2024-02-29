using Game.Inventory.Cells.CellsContainer.Views;
using UnityEngine;

namespace Game.Inventory.HotBar.Views
{
    public class InventoryHotBarView : MonoBehaviour
    {
        [Header("Cells container")]
        [SerializeField] private CellsContainerView _cellsContainerView;

        public CellsContainerView CellsContainerView => _cellsContainerView;
    }
}
