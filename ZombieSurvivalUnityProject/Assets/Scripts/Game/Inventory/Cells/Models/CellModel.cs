using Core.SaveSystem.Models;

namespace Game.Inventory.Cells.Models
{
    public partial class CellModel : SaveableModel<CellModel.Data>
    {
        protected override string DataKey => "CellModel.Data";
    }
}