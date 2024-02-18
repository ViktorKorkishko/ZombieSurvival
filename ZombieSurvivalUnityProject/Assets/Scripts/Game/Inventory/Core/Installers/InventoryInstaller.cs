using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [Inject] private IViewProvider ViewProvider { get; }

        [SerializeField] private InventoryView _inventoryViewPrefab;
        
        [SerializeField] private CellView _cellViewPrefab;
        [SerializeField] private int _cellsCount;
        
        public override void InstallBindings()
        {
            Container
                .Bind<InventoryModel>()
                .AsSingle()
                .WithArguments(_cellsCount);
            
            var viewInstance = ViewProvider.RegisterView(_inventoryViewPrefab, ViewId.Inventory);
            
            Container
                .BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle()
                .WithArguments(viewInstance);
            
            Container
                .BindFactory<CellView, CellView.Factory>()
                .FromComponentInNewPrefab(_cellViewPrefab);
        }
    }
}
