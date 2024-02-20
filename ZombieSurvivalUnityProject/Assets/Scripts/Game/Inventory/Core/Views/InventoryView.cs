using System;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Data;
using Game.Inventory.Cells.Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory.Core.Views
{
    public class InventoryView : ViewBase
    {
        [Header("Buttons")] 
        [SerializeField] private Button _deleteButton;
        [SerializeField] private CanvasGroup _deleteButtonCanvasGroup;
        [SerializeField] [Range(0, 1f)] private float _disabledStateAlphaValue;
        
        [Header("Cells")]
        [SerializeField] private Transform _cellsParentTransform;
        
        public Func<CellView> OnCellViewCreated { get; set; }

        public Action OnDeleteItemButtonClicked { get; set; }

        public CellView InitCell()
        {
            var newCellView = OnCellViewCreated?.Invoke();
            if (newCellView != null)
            {
                newCellView.transform.SetParent(_cellsParentTransform);
                return newCellView;
            }

            return null;
        }

        protected override void HandleOnShow(IViewData viewData)
        {
            _deleteButton.onClick.AddListener(TriggerButtonClickEvent);
        }

        protected override void HandleOnHide()
        {
            _deleteButton.onClick.RemoveListener(TriggerButtonClickEvent);
        }

        public void SetDeleteButtonEnabled(bool enabled)
        {
            _deleteButtonCanvasGroup.alpha = enabled ? 1f : _disabledStateAlphaValue;
            _deleteButton.enabled = enabled;
        }

        private void TriggerButtonClickEvent()
        {
            OnDeleteItemButtonClicked?.Invoke();
        }
    }
}
