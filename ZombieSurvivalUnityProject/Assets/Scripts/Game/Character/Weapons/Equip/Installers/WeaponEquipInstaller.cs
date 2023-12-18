using Core.Installers;
using Game.Character.Weapons.Equip.Controllers;
using Game.Character.Weapons.Equip.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Equip.Installers
{
    public class WeaponEquipInstaller : MonoInstaller
    {
        [SerializeField] private WeaponEquipModel _weaponEquipModel;
        
        [SerializeField] private Transform _weaponHolder;

        public override void InstallBindings()
        {
            Container.Bind<WeaponEquipModel>()
                .FromInstance(_weaponEquipModel)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponEquipController>()
                .AsSingle();

            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.WeaponHolder)
                .FromInstance(_weaponHolder);
        }
    }
}
