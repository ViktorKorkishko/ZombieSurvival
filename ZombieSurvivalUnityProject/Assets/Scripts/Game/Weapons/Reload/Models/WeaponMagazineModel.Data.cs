namespace Game.Weapons.Reload.Models
{
    public partial class WeaponMagazineModel
    {
        public new class Data
        {
            public int BulletsLeft { get; set; }
        }

        protected override string DataKey => "WeaponMagazineModel.Data";
    }
}
