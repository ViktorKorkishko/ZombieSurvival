using Game.Weapons.Shooting.Config;
using Game.Weapons.Shooting.Controllers;
using Game.Weapons.Shooting.Models;
using Game.Weapons.Shooting.Views;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shooting.Installer
{
    public class WeaponShootingInstaller : MonoInstaller
    {
        [SerializeField] private WeaponShootingView _view;
        [SerializeField] private WeaponShootingConfig _config;
        [SerializeField] private LayerMask _enemyLayerMask;

        public override void InstallBindings()
        {
            Container.BindInstance(_view).AsSingle();
            Container.BindInstance(_config).AsSingle();
            Container.BindInstance(_enemyLayerMask).AsSingle();
            Container.Bind<WeaponShootingModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponShootingController>().AsSingle();
        }
    }
}
