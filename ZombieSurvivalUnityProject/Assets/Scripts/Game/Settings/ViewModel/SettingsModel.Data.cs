using Core.SaveSystem.Saving.Common.Load;

namespace Game.Settings.ViewModel
{
    public partial class SettingsModel
    {
        public new class Data
        {
            public float Sensitivity { get; set; }
        }
        
        protected override string DataKey => "SettingsViewModel.Data";

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
