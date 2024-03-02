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
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<CharacterCameraRotationController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CrosshairTargetPositionController>().AsSingle();

            Container.BindInstance(_cameraLookAtPointTransform)
                .WithId(BindingIdentifiers.CameraLookAtPointTransform);
        }
    }
}
