using Zenject;

namespace Game.Loading
{
    public class LoadingSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<GameInitializationController>()
                .AsCached();
        }
    }
}