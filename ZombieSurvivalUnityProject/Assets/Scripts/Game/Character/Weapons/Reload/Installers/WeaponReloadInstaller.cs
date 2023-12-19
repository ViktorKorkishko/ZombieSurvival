using Game.Character.Weapons.Reload.Controllers;
using Game.Character.Weapons.Reload.Models;
using Zenject;

namespace Game.Character.Weapons.Reload.Installers
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
