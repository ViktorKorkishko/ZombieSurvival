using Core.Installers;
using Core.SaveSystem.Models;
using Game.Inventory.Cells.CellsContainer.Models;
using Zenject;

namespace Game.Inventory.HotBar.Models
{
    public partial class HotBarModel : SaveableModel<HotBarModel.Data>
    {
        [Inject(Id = BindingIdentifiers.HotBarCellsContainer)] public CellsContainerModel HotBarCellsContainerModel { get; }
        [Inject(Id = BindingIdentifiers.InventoryHotBarCellsContainer)] public CellsContainerModel InventoryHotBarCellsContainer { get; }
        
        public int InitialHotBarCellsCount { get; }
        
        public HotBarModel(int initialHotBarCellsCount)
        {
            InitialHotBarCellsCount = initialHotBarCellsCount;
        }
    }
}