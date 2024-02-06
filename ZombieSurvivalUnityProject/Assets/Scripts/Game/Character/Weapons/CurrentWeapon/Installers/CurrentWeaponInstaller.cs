using Game.Character.Weapons.CurrentWeapon.Models;
using Zenject;

namespace Game.Character.Weapons.CurrentWeapon.Installers
{
    public class CurrentWeaponInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CurrentWeaponModel>().AsSingle();
        }
    }
}
