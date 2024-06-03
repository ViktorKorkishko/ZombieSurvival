using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;

namespace Game.Settings.ViewModel
{
    public partial class SettingsModel : SaveableModel<SettingsModel.Data>
    {
        public float Sensitivity { get; set; } = 1f;
        
        public SettingsModel(SaveableEntity entity) : base(entity) { }
    }
}
