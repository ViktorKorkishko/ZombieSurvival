using Zenject;

namespace Core.Lifetime.Instantiation.Installers
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
