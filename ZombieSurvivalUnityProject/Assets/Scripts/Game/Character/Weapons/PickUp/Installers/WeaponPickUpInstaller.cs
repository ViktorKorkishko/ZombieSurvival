using Game.Character.Weapons.PickUp.Controllers;
using Game.Character.Weapons.PickUp.Models;
using Zenject;

namespace Game.Character.Weapons.PickUp.Installers
{
    public class WeaponPickUpInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponPickUpModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponPickUpController>().AsSingle();
        }
    }
}
