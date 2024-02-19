using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public partial class CellView : MonoBehaviour,
        IBeginDragHandler, IEndDragHandler, IDragHandler,
        IDropHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private CanvasGroup _imageCanvasGroup;
        
        public Action<CellView, Image> OnBeginDrag { get; set; }
        public Action<CellView> OnEndDrag { get; set; }
        public Action<PointerEventData> OnDrag { get; set; }
        public Action<CellView> OnDrop { get; set; }
        
        public void Show()
        {
            _itemImage.gameObject.SetActive(true);
            _countText.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            _itemImage.gameObject.SetActive(false);
            _countText.gameObject.SetActive(false);
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

            OnBeginDrag?.Invoke(this, _itemImage);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _imageCanvasGroup.alpha = 1;
            _imageCanvasGroup.blocksRaycasts = true;
            
            OnEndDrag?.Invoke(this);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            OnDrop?.Invoke(this);
        }
    }
}