using Core.Installers.Ids;
using Game.Cameras.Controllers;
using Game.Cameras.Models;
using UnityEngine;
using Zenject;

namespace Game.Cameras.Installers
{
    public class MainCameraInstaller : MonoInstaller
    {
        [SerializeField] private Transform _crosshairTargetTransform;

        public override void InstallBindings()
        {
            Container.Bind<CameraModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();

            Container.BindInterfacesAndSelfTo<CrosshairTarget>().AsSingle();
            Container.BindInstance(_crosshairTargetTransform)
                     .WithId(BindingIdentifiers.CrosshairTargetTransform);
        }
    }
}
