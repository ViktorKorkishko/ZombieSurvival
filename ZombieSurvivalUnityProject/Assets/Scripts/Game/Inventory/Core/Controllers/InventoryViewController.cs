using Core.Installers;
using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Core.Views;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryViewController : ViewControllerBase<InventoryView>
    {
        [Inject(Id = BindingIdentifiers.InventoryCellsContainer)] private CellsContainerModel CellsContainerModel { get; }

        public InventoryViewController(IView view) : base(view)
        {
        }
    }
}
