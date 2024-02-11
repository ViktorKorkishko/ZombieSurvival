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
            View.OnSliderValueChanged += HandleOnSliderValueChanged;
            
            View.SetSensitivitySliderValue(SettingsModel.Sensitivity);
        }

        protected override void HandleOnHide()
        {
            View.OnSliderValueChanged -= HandleOnSliderValueChanged;
        }

        private void HandleOnSliderValueChanged(float value)
        {
            SettingsModel.Sensitivity = value;
        }
    }
}
