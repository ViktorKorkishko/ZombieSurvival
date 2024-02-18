using System;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Items.Models;
using Zenject;

namespace Game.Inventory.Cells.Core.Controllers
{
    public class CellController : IDisposable
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
            CellModel.OnItemRemoved += HandleOnItemRemoved;
            CellModel.OnItemCountChanged += HandleOnItemCountChanged;
        }

        public void Dispose()
        {
            CellModel.OnItemSet -= HandleOnItemSet;
            CellModel.OnItemRemoved -= HandleOnItemRemoved;
            CellModel.OnItemCountChanged -= HandleOnItemCountChanged;
        }

        private void HandleOnItemRemoved()
        {
            CellView.SetItemImage(null);
            CellView.SetItemCount(0);
        }

        private void HandleOnItemSet(InventoryItemModel inventoryItemModel)
        {
            var sprite = inventoryItemModel.ItemData.Sprite;
            CellView.SetItemImage(sprite);
            CellView.SetItemCount(inventoryItemModel.Count);
        }

        private void HandleOnItemCountChanged(int count)
        {
            CellView.SetItemCount(count);
        }
    }
}
