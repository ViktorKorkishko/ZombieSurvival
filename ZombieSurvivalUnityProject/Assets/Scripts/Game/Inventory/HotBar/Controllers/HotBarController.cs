using System;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using Zenject;

namespace Game.Inventory.HotBar.Controllers
{
    public class HotBarController : IInitializable, IDisposable
    {
        [Inject] private HotBarModel HotBarModel { get; }
        [Inject] private DiContainer DiContainer { get; }

        private HotBarView View { get; }

        private SynchronizableCellsContainer _synchronizableCellsContainer;

        public HotBarController(HotBarView view)
        {
            View = view;
        }

        void IInitializable.Initialize()
        {
            HotBarModel.InitializeCells();

            View.Show();

            _synchronizableCellsContainer = new SynchronizableCellsContainer(
                HotBarModel.InventoryHotBarCellsContainer,
                HotBarModel.HotBarCellsContainerModel);

            DiContainer.Inject(_synchronizableCellsContainer);
            _synchronizableCellsContainer.Initialize();
        }

        void IDisposable.Dispose()
        {
        }

        private void Initialize()
        {
        }
    }
}