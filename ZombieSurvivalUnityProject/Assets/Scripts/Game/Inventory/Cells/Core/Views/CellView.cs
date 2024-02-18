using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public partial class CellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _countText;

        public Action<CellView, PointerEventData> OnBeginDrag { get; set; }
        public Action<CellView, PointerEventData> OnEndDrag { get; set; }

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

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            OnBeginDrag?.Invoke(this, eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            OnEndDrag?.Invoke(this, eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
        }
    }
}