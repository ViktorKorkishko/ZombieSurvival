using System;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Items.Models;
using Game.ItemsDB;
using Zenject;

namespace Game.Inventory.Cells.Core.Controllers
{
    public partial class CellController : IDisposable
    {
        [Inject] private ItemsDataBase ItemsDataBase { get; }

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

        void IDisposable.Dispose()
        {
            CellModel.OnItemSet -= HandleOnItemSet;
            CellModel.OnItemRemoved -= HandleOnItemRemoved;
            CellModel.OnItemCountChanged -= HandleOnItemCountChanged;
            CellModel.OnSelected += HandleOnSelected;
            
            CellView.OnPointerDown -= HandleOnPointerDown;
        }

        private void HandleOnItemSet(InventoryItemModel inventoryItemModel)
        {
            var itemId = inventoryItemModel.ItemId;
            if (ItemsDataBase.TryGetItemData(itemId, out var itemData))
            {
                CellView.SetItemImage(itemData.Sprite);
                CellView.SetItemCount(inventoryItemModel.Count);
            }
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