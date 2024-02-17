using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory.Cells.Core.Views
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _countText;

        public Action<CellView, PointerEventData> OnDown { get; set; }

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

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke(this, eventData);
        }
    }
}