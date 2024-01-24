using Game.Character.Weapons.Shoot.Controllers;
using Game.Character.Weapons.Shoot.Models;
using Zenject;

namespace Game.Character.Weapons.Shoot.Installers
{
    public class CharacterWeaponShootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterWeaponShootModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterWeaponShootController>().AsSingle();
        }
    }
}
