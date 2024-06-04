using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Settings.ViewModel;
using Game.Settings.Views;
using Zenject;

namespace Game.Settings.Controllers
{
    public class SettingsViewController : ViewControllerBase<SettingsView>
    {
        [Inject] private SettingsModel SettingsModel { get; }

        public SettingsViewController(IView view) : base(view) { }

        protected override void HandleOnShow()
        {
            SettingsModel.OnSensitivityChanged += HandleOnSensitivityChanged;
            View.OnSliderValueChanged += HandleOnSliderValueChanged;
            
            View.SetSensitivitySliderValue(SettingsModel.Sensitivity);
        }

        protected override void HandleOnHide(IView view)
        {
            SettingsModel.OnSensitivityChanged -= HandleOnSensitivityChanged;
            View.OnSliderValueChanged -= HandleOnSliderValueChanged;
        }

        private void HandleOnSensitivityChanged(float value)
        {
            View.SetSensitivitySliderValue(value);
        }

        private void HandleOnSliderValueChanged(float value)
        {
            SettingsModel.SetSensitivity(value);
        }
    }
}
