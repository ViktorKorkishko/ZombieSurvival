using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Inventory.Core.Views;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryViewController : ViewControllerBase<InventoryView>
    {
        public InventoryViewController(IView view) : base(view) { }
    }
}
