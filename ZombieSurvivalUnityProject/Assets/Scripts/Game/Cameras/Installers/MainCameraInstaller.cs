using Core.Installers;
using Game.Cameras.Controllers;
using Game.Cameras.Models;
using Game.Crosshair;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Installers
{
    public class MainCameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraModel _cameraModel;
        
        [SerializeField] private Transform _crosshairTargetTransform;
        [SerializeField] private Transform _cameraLookAtPointTransform;

        public override void InstallBindings()
        {
            Container.BindInstance(_cameraModel).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();

            Container.BindInterfacesAndSelfTo<CrosshairTargetPositionController>().AsSingle();
            
            Container.BindInstance(_crosshairTargetTransform)
                .WithId(BindingIdentifiers.CrosshairTargetPointTransform);

            Container.BindInstance(_cameraLookAtPointTransform)
                .WithId(BindingIdentifiers.CameraLookAtPointTransform);
        }
    }
}
