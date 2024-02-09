using System;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Settings.Views
{
    public class SettingsView : ViewBase
    {
        [SerializeField] private Button _someButton;
        [SerializeField] private Slider _sensitivitySlider;
        
        [SerializeField] private Button _closeButton;

        public event Action<float> OnSliderValueChanged;
        public event Action OnCloseButtonClicked;

        protected override void HandleOnShow(IViewData viewData)
        {
            _closeButton.onClick.AddListener(InvokeCloseButtonClicked);
            _sensitivitySlider.onValueChanged.AddListener(InvokeSliderValueChanged);
        }

        protected override void HandleOnHide()
        {
            _closeButton.onClick.RemoveListener(InvokeCloseButtonClicked);
            _sensitivitySlider.onValueChanged.RemoveListener(InvokeSliderValueChanged);
        }

        public void SetSensitivitySliderValue(float value)
        {
            _sensitivitySlider.value = value;
        }

        private void InvokeCloseButtonClicked()
        {
            OnCloseButtonClicked?.Invoke();
        }

        private void InvokeSliderValueChanged(float value)
        {
            OnSliderValueChanged?.Invoke(value);
        }
    }
}
