using Core.Installers;
using Game.Character.Cameras.Controllers;
using Game.Crosshair;
using UnityEngine;
using Zenject;

namespace Game.Character.Cameras.Installers
{
    public class CharacterCameraInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraLookAtPointTransform;
        [SerializeField] private Transform _rigLookAtPointTransform;

        [SerializeField] private Vector3 _offset;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<CharacterCameraRotationController>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<CrosshairTargetPositionController>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<RigLootAtPointController>()
                .AsSingle()
                .WithArguments(_rigLookAtPointTransform, _offset);

            Container.BindInstance(_cameraLookAtPointTransform)
                .WithId(BindingIdentifiers.CameraLookAtPointTransform);
        }
    }
}
