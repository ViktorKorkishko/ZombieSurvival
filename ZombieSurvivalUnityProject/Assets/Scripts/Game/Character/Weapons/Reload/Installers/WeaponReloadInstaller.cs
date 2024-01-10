using Core.Installers;
using Game.Character.Weapons.Reload.Controllers;
using Game.Character.Weapons.Reload.Models;
using Game.Character.Weapons.Reload.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Reload.Installers
{
    public class WeaponReloadInstaller : MonoInstaller
    {
        [SerializeField] private WeaponReloadAnimationView _weaponReloadAnimationView;
        [SerializeField] private Transform _characterLeftHandTranform;
        
        public override void InstallBindings()
        {
            Container.Bind<WeaponReloadModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponReloadController>().AsSingle();
            
            Container.BindInstance(_weaponReloadAnimationView).AsSingle();
            Container.BindInstance(_characterLeftHandTranform).WithId(BindingIdentifiers.CharacterLeftHandTransform);
        }
    }
}
