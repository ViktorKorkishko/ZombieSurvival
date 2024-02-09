using Core.SaveSystem.Saving.Common.Load;

namespace Game.Settings.ViewModel
{
    public partial class SettingsModel
    {
        public new class Data
        {
            public float CameraSensitivity { get; set; }
        }

        protected override string DataKey => "SettingsViewModel.Data";

        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            Sensitivity = base.Data.CameraSensitivity;
        }

        protected override void HandleOnDataPreSaved()
        {
            base.Data.CameraSensitivity = Sensitivity;
        }
    }
}
