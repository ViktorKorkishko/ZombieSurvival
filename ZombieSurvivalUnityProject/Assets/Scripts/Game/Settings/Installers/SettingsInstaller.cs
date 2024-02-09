using Game.Settings.ViewModel;
using Zenject;

namespace Game.Settings.Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle();
        }
    }
}
