using Game.Inventory.Core.Models;
using Zenject;

namespace Game.Inventory.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InventoryModel>().AsSingle();
        }
    }
}
