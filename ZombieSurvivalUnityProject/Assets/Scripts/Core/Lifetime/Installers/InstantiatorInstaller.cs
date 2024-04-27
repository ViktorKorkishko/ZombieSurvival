using Zenject;

namespace Core.Lifetime.Installers
{
    public class InstantiatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Instantiator>()
                .AsSingle();
        }
    }
}
