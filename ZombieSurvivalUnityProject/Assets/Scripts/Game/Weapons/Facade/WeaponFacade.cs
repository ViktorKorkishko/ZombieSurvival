using Core.Installers;
using Core.Lifetime.Facade;
using Game.Items.Data;
using Game.Items.Enums;
using Game.Weapons.Common;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shoot.Models;
using UnityEngine;

namespace Game.Weapons.Facade
{
    public class WeaponFacade : FacadeBase
    {
        public WeaponId WeaponId => LocalDiContainer.Resolve<WeaponId>();
        public Transform Root => LocalDiContainer.ResolveId<Transform>(BindingIdentifiers.Root);
        
        public WeaponReloadModel ReloadModel => LocalDiContainer.Resolve<WeaponReloadModel>();
        public WeaponMagazineModel WeaponMagazineModel => LocalDiContainer.Resolve<WeaponMagazineModel>();
        public GameObject MagazineGameObject => LocalDiContainer.ResolveId<GameObject>(BindingIdentifiers.MagazineGameObject);
        
        public WeaponShootModel ShootModel => LocalDiContainer.Resolve<WeaponShootModel>();
        
        private WeaponConfig WeaponConfig => LocalDiContainer.Resolve<WeaponConfig>();
        
        public override void Init(ItemData data)
        {
            WeaponMagazineModel.LoadBullets(WeaponConfig.MagazineSize);
        }
    }
}
