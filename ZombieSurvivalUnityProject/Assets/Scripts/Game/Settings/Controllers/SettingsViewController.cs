using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Hotkeys;
using Game.Hotkeys.Models;
using Game.Settings.ViewModel;
using Game.Settings.Views;
using UnityEngine;
using Zenject;

namespace Game.Settings.Controllers
{
    public class SettingsViewController : ViewControllerBase<SettingsView>
    {
        [Inject] private SettingsModel SettingsModel { get; }
        [Inject] private HotKeysModel HotKeysModel { get; }

        public SettingsViewController(IView view) : base(view) { }

        public override void Initialize()
        {
            base.Initialize();
            
            HotKeysModel.OverrideHotKey(KeyCode.Escape, () => View.Show(), this);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            HotKeysModel.ClearHotKey(KeyCode.Escape, this);
        }

        protected override void HandleOnShow()
        {
            SettingsModel.OnSensitivityChanged += HandleOnSensitivityChanged;
            View.OnSliderValueChanged += HandleOnSliderValueChanged;
            
            HotKeysModel.OverrideHotKey(KeyCode.Escape, View.Hide, this);
            
            View.SetSensitivitySliderValue(SettingsModel.Sensitivity);
        }

        protected override void HandleOnHide(IView view)
        {
            SettingsModel.OnSensitivityChanged -= HandleOnSensitivityChanged;
            View.OnSliderValueChanged -= HandleOnSliderValueChanged;
            
            HotKeysModel.ClearHotKey(KeyCode.Escape, this);
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
