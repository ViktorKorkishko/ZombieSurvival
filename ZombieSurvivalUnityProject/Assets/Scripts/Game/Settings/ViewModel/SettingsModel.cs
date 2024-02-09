using Core.SaveSystem.Models;

namespace Game.Settings.ViewModel
{
    public partial class SettingsModel : SaveableModel<SettingsModel.Data>
    {
        public float Sensitivity { get; set; }
    }
}
