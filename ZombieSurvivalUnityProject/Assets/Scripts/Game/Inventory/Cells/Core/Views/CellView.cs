using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public partial class CellView : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [Header("Item")]
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _countText;
        
        [Header("Drag and drop")]
        [SerializeField] private CanvasGroup _imageCanvasGroup;

        [Header("Select")]
        [SerializeField] private Image _selectImage;
        
        public Action OnPointerDown { get; set; }
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

        public void SetEmpty()
        {
            _itemImage.sprite = null;
            _itemImage.enabled = false;

            _countText.text = string.Empty;
            _countText.enabled = false;
        }

        public void SetItemImage(Sprite sprite)
        {
            _itemImage.sprite = sprite;
            _itemImage.enabled = true;
        }

        public void SetItemCount(int count)
        {
            _countText.enabled = true;
            _countText.text = count.ToString();
        }

        public void SetSelected(bool selected)
        {
            _selectImage.enabled = selected;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnPointerDown?.Invoke();
        }

        #region Drag and drop callbacks

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

        #endregion
    }
}