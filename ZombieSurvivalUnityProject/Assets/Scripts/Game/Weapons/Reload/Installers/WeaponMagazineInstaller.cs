using Game.Weapons.Reload.Models;
using Zenject;

namespace Game.Weapons.Reload.Installers
{
    public class WeaponMagazineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<WeaponMagazineModel>()
                .AsSingle();
        }
    }
}
