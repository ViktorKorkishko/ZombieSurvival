using System.Collections.Generic;
using Core.Installers.Ids;
using Game.Character.Movement.Rotation.Controllers;
using Game.Character.Movement.Rotation.Models;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Movement.Rotation.Installers
{
    public class CharacterRotationInstaller : MonoInstaller
    {
        [SerializeField] private CharacterRotationModel _characterRotationModel;
        
        [SerializeField] private Transform _characterRigRootTransform;
        [SerializeField] private List<MultiAimConstraint> _lookConstraints;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterRotationModel>()
                .FromInstance(_characterRotationModel)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterRotationController>().AsSingle();

            Container.BindInstance(_characterRigRootTransform)
                .WithId(BindingIdentifiers.CharacterRigRoot)
                .AsSingle();
            Container.BindInstances(_lookConstraints);

        }
    }
}
