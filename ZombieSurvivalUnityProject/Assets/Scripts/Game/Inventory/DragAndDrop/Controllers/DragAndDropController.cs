using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Game.Inventory.DragAndDrop.Controllers
{
    public class DragAndDropController : IDisposable
    {
        private readonly Dictionary<CellView, CellModel> _cellViewToModelDictionary = new();
        private readonly Transform _inventoryRoot;
        
        private DraggedViewData _draggedViewData;
        
        public DragAndDropController(Transform inventoryRoot, IEnumerable<CellContainer> cellContainers)
        {
            _inventoryRoot = inventoryRoot;

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

        private void HandleOnDrop(CellView droppedToCellView, PointerEventData eventData)
        {
            var draggedView = _draggedViewData?.View;
            if (draggedView == null)
                return;

            if (!_cellViewToModelDictionary.TryGetValue(draggedView, out var draggedFromCell))
                return;
            
            if (!_cellViewToModelDictionary.TryGetValue(droppedToCellView, out var droppedToCell))
            {
                EndDrag();
                return;
            }
            
            var draggedItem = draggedFromCell.RemoveItem();
            droppedToCell.SetItem(draggedItem);
            
            EndDrag();
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
