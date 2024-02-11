using Game.Inventory.Cells.Models;
using Zenject;

namespace Game.Inventory.Cells.Installers
{
    public class CellInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CellModel>().AsCached();
        }
    }
}
