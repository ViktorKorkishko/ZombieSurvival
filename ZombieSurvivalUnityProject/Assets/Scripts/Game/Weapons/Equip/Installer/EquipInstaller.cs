using Core.Installers;
using Game.Weapons.Equip.Controllers;
using Game.Weapons.Equip.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Equip.Installer
{
    public class EquipInstaller : MonoInstaller
    {
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private Transform _leftHandGripTansform;
        [SerializeField] private Transform _rightHandGripTansform;
        
        public override void InstallBindings()
        {
            Container.Bind<WeaponEquipModel>().AsSingle();
            Container.Bind<WeaponEquipController>().AsSingle();
            
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.Root)
                .FromInstance(_weaponRoot);
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.LeftHandGripTransform)
                .FromInstance(_leftHandGripTansform);
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.RightHandGripTransform)
                .FromInstance(_rightHandGripTansform);
        }
    }
}
