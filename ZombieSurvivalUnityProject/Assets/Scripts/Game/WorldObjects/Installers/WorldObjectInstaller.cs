using Game.WorldObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.WorldObjects.Installers
{
    public class WorldObjectInstaller : MonoInstaller
    {
        [SerializeField] private WorldObjectModel _worldObjectModel;
        
        public override void InstallBindings()
        {
            Container
                .Bind<WorldObjectModel>()
                .FromInstance(_worldObjectModel)
                .AsSingle();
            
            _worldObjectModel.InitDiContainer(Container);
        }
    }
}
