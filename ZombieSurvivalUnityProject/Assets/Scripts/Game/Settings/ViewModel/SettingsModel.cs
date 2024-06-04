using System;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;

namespace Game.Settings.ViewModel
{
    public partial class SettingsModel : SaveableModel<SettingsModel.Data>
    {
        public float Sensitivity { get; private set; }
        
        public event Action<float> OnSensitivityChanged;
        
        private float defaultSensitivity => 1f;
        
        public SettingsModel(SaveableEntity entity) : base(entity) { }

        public void SetSensitivity(float value)
        {
            Sensitivity = value;
            OnSensitivityChanged?.Invoke(value);
        }
    }
}
