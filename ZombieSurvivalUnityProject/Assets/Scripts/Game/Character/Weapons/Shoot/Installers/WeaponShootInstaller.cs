using Game.Character.Weapons.Shoot.Controllers;
using Game.Character.Weapons.Shoot.Models;
using Zenject;

namespace Game.Character.Weapons.Shoot.Installers
{
    public class WeaponShootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponShootModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponShootController>().AsSingle();
        }
    }
}
