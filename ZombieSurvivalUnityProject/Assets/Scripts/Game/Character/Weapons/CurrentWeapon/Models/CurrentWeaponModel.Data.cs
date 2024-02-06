using Game.Weapons.Common;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public partial class CurrentWeaponModel
    {
        public new class Data
        {
            public WeaponId WeaponId { get; set; } = WeaponId.None;
        }

        protected override string DataKey => "CurrentWeaponModel.Data";
    }
}
