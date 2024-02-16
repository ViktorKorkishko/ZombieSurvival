using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Items.Models;
using Zenject;

namespace Game.Inventory.Cells.Core.Controllers
{
    public class CellController
    {
        [Inject] private CellModel CellModel { get; }
        [Inject] private CellView CellView { get; }
        
        public CellController(CellModel model, CellView view)
        {
            CellModel = model;
            CellView = view;
        }

        public void Init()
        {
            CellModel.OnItemSet += HandleOnItemSet;
        }

        public void Dispose()
        {
            CellModel.OnItemSet -= HandleOnItemSet;
        }

        private void HandleOnItemSet(InventoryItemModel inventoryItemModel)
        {
            var sprite = inventoryItemModel.ItemData.Sprite;
            CellView.SetItemImage(sprite);
        }
    }
}