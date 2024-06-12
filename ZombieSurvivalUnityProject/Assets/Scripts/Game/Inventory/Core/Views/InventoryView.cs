using System;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Data;
using Game.Inventory.Cells.CellsContainer.Views;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Inventory.Core.Views
{
    public class InventoryView : ViewBase
    {
        [Header("Cells container")]
        [SerializeField] private CellsContainerView _inventoryCellsContainerView;
        [SerializeField] private CellsContainerView _hotBarCellsContainerView;
        
        [Header("Buttons")] 
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _dropButton;
        [SerializeField] private CanvasGroup _deleteButtonCanvasGroup;
        [SerializeField] private CanvasGroup _dropButtonCanvasGroup;
        
        [Header("Buttons settings")]
        [SerializeField] [Range(0, 1f)] private float _disabledStateAlphaValue;

        public CellsContainerView InventoryCellsContainerView => _inventoryCellsContainerView;
        public CellsContainerView HotBarCellsContainerView => _hotBarCellsContainerView;

        public event Action OnDeleteItemButtonClicked;
        public event Action OnDropItemButtonClicked;

        private float _enabledStateAlphaValue => 1f;
        
        protected override void HandleOnShow(IViewData viewData)
        {
            _deleteButton.onClick.AddListener(TriggerDeleteButton);
            _dropButton.onClick.AddListener(TriggerDropButton);
        }
        
        protected override void HandleOnHide()
        {
            _deleteButton.onClick.RemoveListener(TriggerDeleteButton);
            _dropButton.onClick.RemoveListener(TriggerDropButton);
        }

        public void SetDeleteButtonEnabled(bool enabled)
        {
            _deleteButtonCanvasGroup.alpha = enabled ? _enabledStateAlphaValue : _disabledStateAlphaValue;
            _deleteButton.enabled = enabled;
        }

        public void SetDropButtonEnabled(bool enabled)
        {
            _dropButtonCanvasGroup.alpha = enabled ? _enabledStateAlphaValue : _disabledStateAlphaValue;
            _dropButton.enabled = enabled;
        }

        private void TriggerDeleteButton()
        {
            OnDeleteItemButtonClicked?.Invoke();
        }
        
        private void TriggerDropButton()
        {
            OnDropItemButtonClicked?.Invoke();
        }
    }
}
