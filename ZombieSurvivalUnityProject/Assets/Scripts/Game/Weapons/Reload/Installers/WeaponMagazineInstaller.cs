using Game.Weapons.Reload.Controllers;
using Game.Weapons.Reload.Models;
using Zenject;

namespace Game.Weapons.Reload.Installer
{
    public class WeaponMagazineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponMagazineModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponMagazineController>().AsSingle();
        }
    }
}
