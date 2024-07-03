using Game.Hotkeys.Models;
using Zenject;

namespace Game.Hotkeys.Installers
{
    public class HotKeysInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<HotKeysModel>()
                .AsSingle();
        }
    }
}
