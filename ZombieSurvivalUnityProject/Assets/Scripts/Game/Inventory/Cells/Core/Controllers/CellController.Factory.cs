using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Zenject;

namespace Game.Inventory.Cells.Core.Controllers
{
    public partial class CellController
    {
        public class Factory : PlaceholderFactory<CellModel, CellView, CellController> { }
    }
}
