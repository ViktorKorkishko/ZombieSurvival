using Zenject;

namespace Game.Hotkeys.Installers
{
    public class HotkeysInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<HotKeysModel>()
                .AsSingle();
        }
    }
}