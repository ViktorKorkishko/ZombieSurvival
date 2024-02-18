using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

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
        
        private void HandleOnBeginDrag(CellView cellView, RectTransform itemImageRectTransform, Vector2 itemImagePosition)
        {
            if (_cellViewToModelDictionary.TryGetValue(cellView, out var draggedFromCell))
            {
                if (!draggedFromCell.ContainsItem)
                {
                    _draggedViewData = null;
                    return;
                }
            }
            else
            {
                _draggedViewData = null;
                return;
            }
            
            // save item image previous parent
            // detach item image
            
            var imageParent = itemImageRectTransform.parent;
            var imagePosition = itemImageRectTransform.anchoredPosition;
            itemImageRectTransform.SetParent(_inventoryRoot);
            itemImageRectTransform.SetAsLastSibling();

            _draggedViewData = new DraggedViewData(cellView, imageParent, imagePosition);
        }
        
        private void HandleOnDrag(RectTransform itemImageRectTransform, PointerEventData eventData)
        {
            // change item image position

            itemImageRectTransform.anchoredPosition += eventData.delta;
        }

        private void HandleOnDrop(CellView cellView, PointerEventData eventData)
        {
            var view = _draggedViewData?.View;
            if (view == null)
                return;

            if (!_cellViewToModelDictionary.TryGetValue(view, out var droppedFromCell))
                return;
            
            if (!droppedFromCell.ContainsItem)
                return;

            if (_cellViewToModelDictionary.TryGetValue(cellView, out var droppedToCell))
            {
                var draggedItem = droppedFromCell.RemoveItem();
                droppedToCell.SetItem(draggedItem);

                _draggedViewData = new DraggedViewData(null, _draggedViewData.ImageParent, _draggedViewData.ImagePosition);
            }
        }

        private void HandleOnEndDrag(RectTransform itemImageRectTransform)
        {
            // restore parent and position
            
            if (_draggedViewData == null)
                return;

            itemImageRectTransform.SetParent(_draggedViewData.ImageParent);
            itemImageRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
