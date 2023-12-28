using Core.Installers;
using Game.Weapons.Common.Config;
using Game.Weapons.Shoot.Controllers;
using Game.Weapons.Shoot.Models;
using Game.Weapons.Shoot.Views;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shoot.Installers
{
    public class WeaponShootInstaller : MonoInstaller
    {
        [SerializeField] private WeaponShootView _view;
        
        [SerializeField] private WeaponConfig _config;
        [SerializeField] private LayerMask _enemyLayerMask;
        
        [SerializeField] private ParticleSystem _muzzleFlashEffect;
        [SerializeField] private ParticleSystem _shotHitEffect;

        public override void InstallBindings()
        {
            Container.Bind<WeaponShootModel>().AsSingle();
            Container.BindInstance(_view).AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponShootController>().AsSingle();
            
            Container.BindInstance(_config).AsSingle();
            Container.BindInstance(_enemyLayerMask).AsSingle();
            Container.BindInstance(_muzzleFlashEffect)
                .WithId(BindingIdentifiers.MuzzleFleshPS);
            Container.BindInstance(_shotHitEffect)
                .WithId(BindingIdentifiers.ShotHitEffectPS);
        }
    }
}
