using System;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Data;
using Game.Inventory.Cells.CellsContainer.Views;
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
        
        [Header("Cells container")]
        [SerializeField] private CellsContainerView _cellsContainerView;

        public CellsContainerView CellsContainerView => _cellsContainerView;
        
        public Action OnDeleteItemButtonClicked { get; set; }
        
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
