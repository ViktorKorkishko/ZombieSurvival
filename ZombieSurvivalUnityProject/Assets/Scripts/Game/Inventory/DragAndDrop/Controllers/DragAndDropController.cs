using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using Game.ItemsDB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Game.Inventory.DragAndDrop.Controllers
{
    // callbacks execution order
    // 1. HandleOnBeginDrag
    // 2. HandleOnDrag
    // 3. HandleOnDrop
    // 4. HandleOnEndDrag

    // TODO: refactor (add dependencies have to be injected)
    public class DragAndDropController
    {
        private ItemsDataBase ItemsDataBase { get; }
        private Transform InventoryRoot { get; }
        private Dictionary<CellView, CellModel> _cellViewToModelDictionary { get; } = new();

        private DragData _dragData;
        
        // TODO: refactor later and register cells through model (add CellsContainer module (model, view, controller))
        public DragAndDropController(Transform inventoryRoot, IEnumerable<CellContainer> cellContainers,
            ItemsDataBase itemsDataBase)
        {
            InventoryRoot = inventoryRoot;
            ItemsDataBase = itemsDataBase;

            foreach (var cellContainer in cellContainers)
            {
                _cellViewToModelDictionary.Add(cellContainer.View, cellContainer.Model);
            }
        }

        public void Init()
        {
            _cellViewToModelDictionary.Keys.ToList().ForEach(x =>
            {
                x.OnBeginDrag += HandleOnBeginDrag;
                x.OnEndDrag += HandleOnEndDrag;
                x.OnDrag += HandleOnDrag;
                x.OnDrop += HandleOnDrop;
            });
        }

        private void StartDrag(CellView cellView, Image itemImage)
        {
            _dragData = new DragData(cellView, itemImage, InventoryRoot);
            cellView.Hide();
        }

        private void EndDrag()
        {
            Object.Destroy(_dragData.DraggedImageGO);
            _dragData.View.Show();
            _dragData = null;
        }

        #region Drag and drop callbacks

        private void HandleOnBeginDrag(CellView cellView, Image itemImage)
        {
            if (_cellViewToModelDictionary.TryGetValue(cellView, out var draggedFromCell))
            {
                // cell has no item, not starting drag
                if (draggedFromCell.ContainsItem)
                {
                    StartDrag(cellView, itemImage);
                }
            }
            else // cell not registered, not starting drag
            {
                EndDrag();
            }
        }

        private void HandleOnDrag(PointerEventData eventData)
        {
            // nothing is being drag
            if (_dragData == null)
                return;

            _dragData.DraggedItemImageRectTransform.anchoredPosition += eventData.delta;
        }

        private void HandleOnDrop(CellView draggedToCellView)
        {
            // nothing is being drag
            if (_dragData == null)
                return;

            var draggedFromCellView = _dragData.View;
            var draggedFromCell = _cellViewToModelDictionary[draggedFromCellView];
            var draggedToCell = _cellViewToModelDictionary[draggedToCellView];

            HandleItemDrag(draggedFromCell, draggedToCell);

            EndDrag();
        }

        private void HandleOnEndDrag(CellView draggedCellView)
        {
            // nothing is being drag
            if (_dragData == null)
                return;

            draggedCellView.Show();
            EndDrag();
        }

        private void HandleItemDrag(CellModel fromCell, CellModel toCell)
        {
            // item has been dragged to same cell
            if (fromCell == toCell)
                return;

            bool cellToIsEmpty = !toCell.ContainsItem;
            if (cellToIsEmpty)
            {
                MoveItemFromCellToCell(fromCell, toCell);
                return;
            }

            bool sameItems = fromCell.ItemId == toCell.ItemId;
            if (sameItems)
            {
                if (!ItemsDataBase.TryGetItemData(fromCell.ItemId, out var itemData))
                    return;

                int fullStackCount = itemData.MaxStackCount;
                bool oneIsFullyStacked = fromCell.ItemCount == fullStackCount ||
                                         toCell.ItemCount == fullStackCount;
                if (oneIsFullyStacked)
                {
                    SwapItems(fromCell, toCell);
                }
                else
                {
                    int freeSlotsCount = fullStackCount - toCell.ItemCount;
                    int itemsCountThatCanBeReplaced = freeSlotsCount >= fromCell.ItemCount
                        ? fromCell.ItemCount
                        : fullStackCount - toCell.ItemCount;

                    toCell.AdjustItemCount(itemsCountThatCanBeReplaced);
                    fromCell.AdjustItemCount(-itemsCountThatCanBeReplaced);
                }
            }
            else
            {
                SwapItems(fromCell, toCell);
            }
        }

        #endregion

        private void MoveItemFromCellToCell(CellModel fromCell, CellModel toCell)
        {
            var draggedItem = fromCell.RemoveItem();
            toCell.SetItem(draggedItem);
        }

        private void SwapItems(CellModel fromCell, CellModel toCell)
        {
            var fromCellItem = fromCell.RemoveItem();
            var toCellItem = toCell.RemoveItem();

            toCell.SetItem(fromCellItem);
            fromCell.SetItem(toCellItem);
        }
    }
}