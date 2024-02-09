using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Settings.ViewModel;
using Game.Settings.Views;
using UnityEngine;
using Zenject;

namespace Game.Settings.Controllers
{
    public class SettingsViewController : ViewControllerBase<SettingsView>
    {
        [Inject] private SettingsModel SettingsModel { get; }

        public SettingsViewController(IView view) : base(view) { }

        protected override void HandleOnShow()
        {
            View.OnSliderValueChanged += HandleOnSliderValueChanged;
            View.OnCloseButtonClicked += HandleOnCloseButtonClicked;
            
            View.SetSensitivitySliderValue(SettingsModel.Sensitivity);
        }

        protected override void HandleOnHide()
        {
            View.OnSliderValueChanged -= HandleOnSliderValueChanged;
            View.OnCloseButtonClicked -= HandleOnCloseButtonClicked;
        }

        private void HandleOnCloseButtonClicked()
        {
            View.Hide();
        }

        private void HandleOnSliderValueChanged(float value)
        {
            SettingsModel.Sensitivity = value;
        }
    }
}
