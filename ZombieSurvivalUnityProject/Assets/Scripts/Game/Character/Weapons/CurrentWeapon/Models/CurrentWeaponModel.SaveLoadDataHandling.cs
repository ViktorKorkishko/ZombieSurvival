using Core.SaveSystem.Saving.Common.Load;
using Game.Weapons.Common;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public partial class CurrentWeaponModel
    {
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    RestorePlayerWeapon();
                    break;
            }
        
            void RestorePlayerWeapon()
            {
                // spawn weapon
            }
        }
        
        protected override void HandleOnDataPreSaved()
        {
            base.Data.WeaponId = IsWeaponEquipped ? Weapon.GetComponent<WeaponId>() : WeaponId.None;
        }
    }
}
