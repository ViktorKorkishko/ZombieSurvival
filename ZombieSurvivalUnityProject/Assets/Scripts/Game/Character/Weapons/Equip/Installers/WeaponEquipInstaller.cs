using System.Collections;
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
        
        [SerializeField] private Animator _rigAnimator;
        
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private string _unarmedStateName;

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponEquipModel)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponEquipController>()
                .AsSingle();

            Container.BindInstance(_rigAnimator)
                .WithId(BindingIdentifiers.CharacterRigAnimator);

            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.WeaponHolder)
                .FromInstance(_weaponHolder);
            
            Container.BindInstance(_unarmedStateName).WithId(BindingIdentifiers.UnarmedStateName);
        }
    }
}
