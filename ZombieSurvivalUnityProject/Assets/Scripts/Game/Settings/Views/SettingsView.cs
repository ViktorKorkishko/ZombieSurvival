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
        
        public event Action<float> OnSliderValueChanged;

        protected override void HandleOnShow(IViewData viewData)
        {
            _sensitivitySlider.onValueChanged.AddListener(InvokeSliderValueChanged);
        }

        protected override void HandleOnHide()
        {
            _sensitivitySlider.onValueChanged.RemoveListener(InvokeSliderValueChanged);
        }

        public void SetSensitivitySliderValue(float value)
        {
            _sensitivitySlider.value = value;
        }

        private void InvokeSliderValueChanged(float value)
        {
            OnSliderValueChanged?.Invoke(value);
        }
    }
}
