using Core.Installers;
using Game.Weapons.Reload.Controllers;
using Game.Weapons.Reload.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Installers
{
    public class WeaponReloadInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _magazineGameObject;

        public override void InstallBindings()
        {
            Container.Bind<WeaponReloadModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponReloadController>().AsSingle();

            Container.BindInstance(_magazineGameObject).WithId(BindingIdentifiers.MagazineGameObject);
        }
    }
}
