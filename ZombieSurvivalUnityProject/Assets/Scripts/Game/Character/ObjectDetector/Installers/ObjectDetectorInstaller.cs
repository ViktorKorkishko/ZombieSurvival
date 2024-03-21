using Game.Character.ObjectDetector.Controllers;
using Game.Character.ObjectDetector.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.ObjectDetector.Installers
{
    public class ObjectDetectorInstaller : MonoInstaller
    {
        [SerializeField] private ObjectDetectorModel _objectDetectorModel;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ObjectDetectorModel>()
                .FromInstance(_objectDetectorModel)
                .AsSingle();
            
            Container
                .BindInterfacesTo<ObjectDetectorController>()
                .AsSingle();
        }
    }
}
