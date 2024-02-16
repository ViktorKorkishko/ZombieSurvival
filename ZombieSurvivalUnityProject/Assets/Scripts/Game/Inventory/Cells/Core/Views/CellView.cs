using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _itemImage;

        public Action<CellView, PointerEventData> OnDown { get; set; }

        public void SetItemImage(Sprite sprite)
        {
            _itemImage.sprite = sprite;
            _itemImage.enabled = sprite != null;
            Debug.Log($"-{gameObject.name}-: Image set");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke(this, eventData);
        }
    }
}
