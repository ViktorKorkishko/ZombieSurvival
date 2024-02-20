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
            CellModel.OnSelected += HandleOnSelected;

            CellView.OnPointerDown += HandleOnPointerDown;
        }

        public void Dispose()
        {
            CellModel.OnItemSet -= HandleOnItemSet;
            CellModel.OnItemRemoved -= HandleOnItemRemoved;
            CellModel.OnItemCountChanged -= HandleOnItemCountChanged;
            
            CellModel.OnSelected += HandleOnSelected;
            CellView.OnPointerDown -= HandleOnPointerDown;
        }

        private void HandleOnItemSet(InventoryItemModel inventoryItemModel)
        {
            var sprite = inventoryItemModel.ItemData.Sprite;
            CellView.SetItemImage(sprite);
            CellView.SetItemCount(inventoryItemModel.Count);
        }

        private void HandleOnItemRemoved()
        {
            CellView.SetEmpty();
        }

        private void HandleOnItemCountChanged(int count)
        {
            CellView.SetItemCount(count);
        }

        private void HandleOnPointerDown()
        {
            CellModel.SetSelected(true);
        }

        private void HandleOnSelected(CellModel cellModel, bool selected)
        {
            CellView.SetSelected(selected);
        }
    }
}
