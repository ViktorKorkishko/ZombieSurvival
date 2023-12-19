using Game.Weapons.Reload.Controllers;
using Game.Weapons.Reload.Models;
using Zenject;

namespace Game.Weapons.Reload.Installers
{
    public class WeaponReloadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponReloadModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponReloadController>().AsSingle();
        }
    }
}
