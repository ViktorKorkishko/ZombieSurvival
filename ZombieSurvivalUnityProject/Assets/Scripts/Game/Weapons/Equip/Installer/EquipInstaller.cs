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
        
        public override void InstallBindings()
        {
            Container.Bind<WeaponEquipModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponEquipController>().AsSingle();
            
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.Root)
                .FromInstance(_weaponRoot);
        }
    }
}
