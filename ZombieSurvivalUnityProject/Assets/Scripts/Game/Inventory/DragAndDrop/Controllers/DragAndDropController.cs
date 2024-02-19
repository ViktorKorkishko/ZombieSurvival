using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using Game.ItemsDB;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DragAndDropController : IDisposable
    {
        [Inject] private ItemsDataBase ItemsDataBase { get; }

        private readonly Dictionary<CellView, CellModel> _cellViewToModelDictionary = new();
        private readonly Transform _inventoryRoot;
        
        private DraggedViewData _draggedViewData;
        
        public DragAndDropController(Transform inventoryRoot, IEnumerable<CellContainer> cellContainers, ItemsDataBase itemsDataBase)
        {
            _inventoryRoot = inventoryRoot;
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

        public void Dispose()
        {
        }

        private void StartDrag(CellView cellView)
        {
            _draggedViewData = new DraggedViewData(cellView, cellView.ItemImage.gameObject);
            cellView.Hide();
        }

        private void EndDrag()
        {
            Object.Destroy(_draggedViewData.DraggedItemImageGO);
            _draggedViewData.View.Show();
            _draggedViewData = null;
        }

        private void HandleOnBeginDrag(CellView cellView, PointerEventData eventData)
        {
            if (_cellViewToModelDictionary.TryGetValue(cellView, out var draggedFromCell))
            {
                if (draggedFromCell.ContainsItem)
                {
                    StartDrag(cellView);
                }
                else
                {
                    return;
                }
            }
            else
            {
                EndDrag();
                return;
            }
            
            // save item image previous parent
            // detach item image
            
            
            // var imageParent = itemImageRectTransform.parent;
            // var imagePosition = itemImageRectTransform.anchoredPosition;
            // itemImageRectTransform.SetParent(_inventoryRoot);
            // itemImageRectTransform.SetAsLastSibling();
        }
        
        private void HandleOnDrag(CellView cellView, PointerEventData eventData)
        {
            // change item image position
            if (_draggedViewData == null)
                return;
            
            _draggedViewData.DraggedItemImageRectTransform.anchoredPosition += eventData.delta;
            // itemImageRectTransform.anchoredPosition += eventData.delta;
        }

        private void HandleOnDrop(CellView draggedToCellView, PointerEventData eventData)
        {
            if (_draggedViewData == null)
                return;
            
            var draggedFromCellView = _draggedViewData.View;
            var draggedFromCell = _cellViewToModelDictionary[draggedFromCellView];
            var draggedToCell = _cellViewToModelDictionary[draggedToCellView];
            
            HandleItemDrag(draggedFromCell, draggedToCell);
            
            EndDrag();
        }

        private void HandleItemDrag(CellModel fromCell, CellModel toCell)
        {
            if (fromCell == toCell)
                return;
            
            bool cellToIsEmpty = !toCell.ContainsItem;
            if (cellToIsEmpty)
            {
                MoveItemFromCellToCell(fromCell, toCell);
                return;
            }
            
            bool sameItems = fromCell.ItemId == toCell.ItemId;
            if (!sameItems)
            {
                SwapItems(fromCell, toCell);
            }
            else
            {
                ItemsDataBase.TryGetItemData(fromCell.ItemId, out var itemData);

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
        }

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

        private void HandleOnEndDrag(CellView draggedCellView, PointerEventData eventData)
        {
            // restore parent and position
            
            if (_draggedViewData == null)
                return;
            
            draggedCellView.Show();
            EndDrag();
            // itemImageRectTransform.SetParent(_draggedViewData.ImageParent);
            // itemImageRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
