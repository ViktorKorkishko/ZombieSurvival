using System;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Items.Models;

namespace Game.Inventory.Cells.Core.Controllers
{
    public class CellController : IDisposable
    {
        private CellModel CellModel { get; }
        private CellView CellView { get; }

        public CellController(CellModel model, CellView view)
        {
            CellModel = model;
            CellView = view;
        }

        public void Init()
        {
            CellModel.OnItemSet += HandleOnItemSet;
            CellModel.OnItemRemoved += HandleOnItemRemoved;
            CellModel.OnItemCountChanged += HandleOnItemCountChanged;
            
            CellModel.Init();
        }

        public void Dispose()
        {
            CellModel.OnItemSet -= HandleOnItemSet;
            CellModel.OnItemRemoved -= HandleOnItemRemoved;
            CellModel.OnItemCountChanged -= HandleOnItemCountChanged;
        }

        private void HandleOnItemSet(InventoryItemModel inventoryItemModel)
        {
            if (inventoryItemModel == null)
            {
                CellModel.RemoveItem();
                return;
            }
            
            var sprite = inventoryItemModel.ItemData.Sprite;
            CellView.SetItemImage(sprite);
            CellView.SetItemCount(inventoryItemModel.Count);
        }

        private void HandleOnItemRemoved()
        {
            CellView.SetItemImage(null);
            CellView.SetItemCount(0);
        }

        private void HandleOnItemCountChanged(int count)
        {
            CellView.SetItemCount(count);
        }
    }
}
