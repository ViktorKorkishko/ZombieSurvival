using Core.Installers;
using Game.Character.Weapons.Reload.Controllers;
using Game.Character.Weapons.Reload.Models;
using Game.Character.Weapons.Reload.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Reload.Installers
{
    public class CharacterWeaponReloadInstaller : MonoInstaller
    {
        [SerializeField] private CharacterWeaponReloadAnimationView _characterWeaponReloadAnimationView;
        [SerializeField] private Transform _characterLeftHandTranform;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterWeaponReloadModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterWeaponReloadController>().AsSingle();
            
            Container.BindInstance(_characterWeaponReloadAnimationView).AsSingle();
            Container.BindInstance(_characterLeftHandTranform).WithId(BindingIdentifiers.CharacterLeftHandTransform);
        }
    }
}
