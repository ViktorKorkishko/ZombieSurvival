using Core.Lifetime.Facade;
using Game.Weapons.Reload.Models;

namespace Game.Weapons.WorldObject.Facade
{
    public class WeaponWorldObjectFacade : FacadeBase<Data>
    {
        private WeaponMagazineModel WeaponMagazineModel => DiContainer.Resolve<WeaponMagazineModel>();

        public override void InitData(Data data)
        {
            WeaponMagazineModel.LoadBullets(data.BulletsCount);
        }
    }
}
