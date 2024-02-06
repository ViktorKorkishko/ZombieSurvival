using Core.SaveSystem.Saving.Common.Load;

namespace Game.Weapons.Reload.Models
{
    public partial class WeaponMagazineModel
    {
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    RestoreBulletsCount();
                    break;
                
                case Result.LoadedWithErrors:
                    FillMagazine();
                    break;
                
                case Result.SaveFileNotFound:
                    FillMagazine();
                    break;
            }
        
            void RestoreBulletsCount()
            {
                BulletsLeft = base.Data.BulletsLeft;
            }
        }
        
        protected override void HandleOnDataPreSaved()
        {
            base.Data.BulletsLeft = BulletsLeft;
        }
    }
}
