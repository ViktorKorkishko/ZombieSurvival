using Core.Installers.Ids;
using Game.Character.Movement.Aim.Controllers;
using Game.Character.Movement.Aim.Models;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Movement.Aim.Installers
{
    public class CharacterAimInstaller : MonoInstaller
    {
        [SerializeField] private CharacterAimModel _characterAimModel;
        [SerializeField] private RaycastWeapon _raycastWeapon;
        [SerializeField] private Rig _aimRig;

        public override void InstallBindings()
        {
            Container.Bind<CharacterAimModel>().FromInstance(_characterAimModel).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterAimController>().AsSingle();

            Container.Bind<RaycastWeapon>().FromInstance(_raycastWeapon).AsSingle();
            Container.Bind<Rig>()
                .WithId(BindingIdentifiers.CharacterAimRig)
                .FromInstance(_aimRig)
                .AsSingle();
        }
    }
}
