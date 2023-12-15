using Game.Cursors.Controllers;
using Game.Cursors.Models;
using Zenject;

namespace Game.Cursors.Installers
{
    public class CursorLockerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CursorLockModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CursorLockController>().AsSingle();
        }
    }
}
