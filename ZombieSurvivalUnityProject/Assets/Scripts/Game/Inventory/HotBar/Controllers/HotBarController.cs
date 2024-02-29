using System.Linq;
using Game.Inventory.Cells;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using UnityEngine;
using Zenject;

namespace Game.Inventory.HotBar.Controllers
{
    public class HotBarController : IInitializable
    {
        [Inject] private HotBarModel HotBarModel { get; }
        private HotBarView View { get; }

        private SelectionController _selectionController;
        private SynchronizableCellsContainer _synchronizableCellsContainer;

        public HotBarController(HotBarView view)
        {
            View = view;
        }

        void IInitializable.Initialize()
        {
            View.Show();

            _synchronizableCellsContainer = new SynchronizableCellsContainer(
                HotBarModel.InventoryHotBarCellsContainer,
                HotBarModel.HotBarCellsContainerModel);

            _synchronizableCellsContainer.Initialize();

            if (HotBarModel.InventoryHotBarCellsContainer.IsInited)
            {
                HandleOnHotBarCellsInitialized();
            }
            else
            {
                HotBarModel.InventoryHotBarCellsContainer.OnInitialized += HandleOnHotBarCellsInitialized;
            }
        }

        private void HandleOnHotBarCellsInitialized()
        {
            HotBarModel.InventoryHotBarCellsContainer.OnInitialized -= HandleOnHotBarCellsInitialized;
            
            _selectionController = new SelectionController(HotBarModel.HotBarCellsContainerModel.Cells);
            _selectionController.OnSelectedCellChanged += HandleOnSelectedCellChanged;
            
            _selectionController.Initialize();
        }

        private void HandleOnSelectedCellChanged(CellModel cellModel)
        {
            var indexOf = HotBarModel.HotBarCellsContainerModel.Cells.ToList().IndexOf(cellModel);
            Debug.Log(indexOf);
        }
    }
}
