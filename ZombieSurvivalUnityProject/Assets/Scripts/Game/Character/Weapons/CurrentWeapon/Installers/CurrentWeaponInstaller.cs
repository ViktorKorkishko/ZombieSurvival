using Game.Character.Weapons.CurrentWeapon.Controllers;
using Game.Character.Weapons.CurrentWeapon.Models;
using Zenject;

namespace Game.Character.Weapons.CurrentWeapon.Installers
{
    public class CurrentWeaponInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CurrentWeaponModel>().AsSingle();
            Container.Bind<CurrentWeaponController>().AsSingle();
        }
    }
}
