using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public partial class CellView : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler, IEndDragHandler, IDragHandler,
        IDropHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private CanvasGroup _imageCanvasGroup;

        public Action<CellView, RectTransform, Vector2> OnBeginDrag { get; set; }
        public Action<RectTransform> OnEndDrag { get; set; }
        public Action<RectTransform, PointerEventData> OnDrag { get; set; }
        public Action<CellView, PointerEventData> OnDrop { get; set; }
        
        public void SetImageId(int id)
        {
            _itemImage.name += id;
        }
        
        public void SetItemImage(Sprite sprite)
        {
            _itemImage.sprite = sprite;
            _itemImage.enabled = sprite != null;
        }

        public void SetItemCount(int count)
        {
            _countText.enabled = count != 0;
            _countText.text = count.ToString();
        }
        
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _imageCanvasGroup.alpha = 0.6f;
            _imageCanvasGroup.blocksRaycasts = false;

            var itemImageRectTransform = _itemImage.rectTransform;
            OnBeginDrag?.Invoke(this, itemImageRectTransform, itemImageRectTransform.anchoredPosition);
            Debug.Log($"{gameObject.name}: OnBeginDrag");
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _imageCanvasGroup.alpha = 1;
            _imageCanvasGroup.blocksRaycasts = true;
            
            OnEndDrag?.Invoke(_itemImage.rectTransform);
            Debug.Log($"{gameObject.name}: OnEndDrag");
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(_itemImage.rectTransform, eventData);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            OnDrop?.Invoke(this, eventData);
            Debug.Log($"{gameObject.name}: OnDrop");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{gameObject.name}: OnPointerDown");
        }
    }
}