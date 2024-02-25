using System;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Cells.CellsContainer.Views;
using Game.Inventory.Cells.Core.Views;
using Zenject;

namespace Game.Inventory.Cells.CellsContainer.Controllers
{
    public class CellsContainerController : IInitializable, IDisposable
    {
        [Inject] private CellView.Factory CellViewFactory { get; }
        
        private CellsContainerModel CellsContainerModel { get; }
        private CellsContainerView CellsContainerView { get; }

        public CellsContainerController(CellsContainerModel cellsContainerModel, CellsContainerView cellsContainerView)
        {
            CellsContainerModel = cellsContainerModel;
            CellsContainerView = cellsContainerView;
        }

        void IInitializable.Initialize()
        {
            CellsContainerModel.OnCellViewInitialized += HandleOnCellInitialized;
            
            CellsContainerView.OnCellViewCreated += HandleOnCellViewCreated;
        }

        void IDisposable.Dispose()
        {
            CellsContainerModel.OnCellViewInitialized -= HandleOnCellInitialized;
            
            CellsContainerView.OnCellViewCreated -= HandleOnCellViewCreated;
        }
        
        private CellView HandleOnCellInitialized()
        {
            var cellView = CellsContainerView.InitCell();
            return cellView;
        }

        private CellView HandleOnCellViewCreated()
        {
            var cellView = CellViewFactory.Create();
            return cellView;
        }
    }
}
