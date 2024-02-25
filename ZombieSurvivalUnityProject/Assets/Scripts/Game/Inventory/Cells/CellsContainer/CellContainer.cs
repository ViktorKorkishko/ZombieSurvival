using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;

namespace Game.Inventory.Cells.CellsContainer
{
    public class CellContainer
    {
        public CellModel Model { get; }
        public CellView View { get; }
        public CellController Controller { get; }
        
        public CellContainer(CellModel model, CellView view, CellController controller)
        {
            Model = model;
            View = view;
            Controller = controller;
        }
    }
}
