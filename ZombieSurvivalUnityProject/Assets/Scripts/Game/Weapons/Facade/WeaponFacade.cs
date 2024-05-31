using Core.Installers;
using Core.Lifetime.Facade;
using Game.Items.Data;
using Game.Weapons.Common;
using Game.Weapons.Common.Config;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shoot.Models;
using UnityEngine;

namespace Game.Weapons.Facade
{
    public class WeaponFacade : FacadeBase
    {
        public WeaponId WeaponId => DiContainer.Resolve<WeaponId>();
        public Transform Root => DiContainer.ResolveId<Transform>(BindingIdentifiers.Root);
        
        public WeaponReloadModel ReloadModel => DiContainer.Resolve<WeaponReloadModel>();
        public WeaponMagazineModel WeaponMagazineModel => DiContainer.Resolve<WeaponMagazineModel>();
        public GameObject MagazineGameObject => DiContainer.ResolveId<GameObject>(BindingIdentifiers.MagazineGameObject);
        
        public WeaponShootModel ShootModel => DiContainer.Resolve<WeaponShootModel>();
        
        private WeaponConfig WeaponConfig => DiContainer.Resolve<WeaponConfig>();
        
        public override void Init(ItemData data)
        {
            WeaponMagazineModel.LoadBullets(WeaponConfig.MagazineSize);
        }
    }
}
