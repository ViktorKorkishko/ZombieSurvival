using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Views;
using UnityEngine;
using Zenject;
using CellModel = Game.Inventory.Cells.Core.Models.CellModel;

namespace Game.Inventory.Cells.Core.Installers
{
    public class CellInstaller : MonoInstaller
    {
        [SerializeField] private CellView _cellView;
        
        public override void InstallBindings()
        {
            Container.Bind<CellModel>().AsSingle();
            Container.Bind<CellView>().FromInstance(_cellView);
            Container.BindInterfacesAndSelfTo<CellController>().AsSingle();
        }
    }
}
