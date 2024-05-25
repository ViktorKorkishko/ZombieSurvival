using Core.Installers;
using Game.Cameras.Controllers;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Installers
{
    public class MainCameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraModel _cameraModel;
        
        [Header("Components")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _crosshairTargetTransform;

        public override void InstallBindings()
        {
            Container
                .Bind<CameraModel>()
                .FromInstance(_cameraModel)
                .AsSingle();
            
            Container
                .BindInstance(_mainCamera)
                .WhenInjectedInto<CameraModel>();

            Container
                .BindInterfacesTo<CameraLockController>()
                .AsSingle();
            
            Container
                .BindInstance(_crosshairTargetTransform)
                .WithId(BindingIdentifiers.CrosshairTargetPointTransform);
        }
    }
}
