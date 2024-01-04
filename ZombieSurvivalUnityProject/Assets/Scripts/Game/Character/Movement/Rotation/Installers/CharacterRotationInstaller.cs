using Core.Installers;
using Game.Character.Movement.Rotation.Controllers;
using Game.Character.Movement.Rotation.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Rotation.Installers
{
    public class CharacterRotationInstaller : MonoInstaller
    {
        [SerializeField] private CharacterRotationModel _characterRotationModel;
        
        [SerializeField] private Transform _characterRigRootTransform;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterRotationModel>()
                .FromInstance(_characterRotationModel)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterRotationController>().AsSingle();

            Container.BindInstance(_characterRigRootTransform)
                .WithId(BindingIdentifiers.CharacterRigRoot)
                .AsSingle();
        }
    }
}
