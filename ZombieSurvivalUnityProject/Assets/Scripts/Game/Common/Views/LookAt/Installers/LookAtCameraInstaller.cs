using UnityEngine;
using Zenject;

namespace Game.Common.Views.LookAt.Installers
{
    public class LookAtCameraInstaller : MonoInstaller
    {
        [SerializeField] private Transform _objectToPoint;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<LootAtCameraController>()
                .AsCached()
                .WithArguments(_objectToPoint);
        }
    }
}