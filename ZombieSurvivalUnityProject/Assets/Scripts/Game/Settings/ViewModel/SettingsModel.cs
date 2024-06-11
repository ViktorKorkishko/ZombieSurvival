using System;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;
using Core.SaveSystem.Saving.Common.Load;

namespace Game.Settings.ViewModel
{
    public class SettingsModel : SaveableModel<SettingsModel.Data>
    {
        public new class Data
        { 
            public float Sensitivity { get; set; } 
        }
        
        public float Sensitivity { get; private set; }
        
        protected override string DataKey => "SettingsViewModel.Data";
        private float defaultSensitivity => 1f;
        
        public event Action<float> OnSensitivityChanged;
        
        public SettingsModel(SaveableEntity entity) : base(entity) { }

        public void SetSensitivity(float value)
        {
            Sensitivity = value;
            OnSensitivityChanged?.Invoke(value);
        }

        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    Sensitivity = base.Data.Sensitivity;
                    break;
                
                default:
                    Sensitivity = defaultSensitivity;
                    break;
            }
        }
        
        protected override void HandleOnDataPreSaved()
        {
            base.Data.Sensitivity = Sensitivity;
        }
    }
}
