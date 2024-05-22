using Core.Installers;
using Core.Lifetime.Facade;
using Game.Items.Enums;
using Game.Weapons.Common;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shoot.Models;
using UnityEngine;

namespace Game.Weapons.Facade
{
    public class WeaponFacade : FacadeBase
    {
        public ItemId ItemId => DiContainer.Resolve<ItemId>();
        public WeaponId WeaponId => DiContainer.Resolve<WeaponId>();
        public Transform Root => DiContainer.ResolveId<Transform>(BindingIdentifiers.Root);
        
        public WeaponReloadModel ReloadModel => DiContainer.Resolve<WeaponReloadModel>();
        public GameObject MagazineGameObject => DiContainer.ResolveId<GameObject>(BindingIdentifiers.MagazineGameObject);
        
        public WeaponShootModel ShootModel => DiContainer.Resolve<WeaponShootModel>();
    }
}
