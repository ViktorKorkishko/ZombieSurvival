using Core.Installers;
using Core.SaveSystem.Entity;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
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

        [Header("View")]
        [SerializeField] private InventoryView _inventoryViewPrefab;
        
        [Header("Cells")]
        [SerializeField] private int _initialCellsCount;
        [SerializeField] private CellView _cellViewPrefab;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InventoryModel>()
                .AsSingle()
                .WithArguments(_initialCellsCount);
            
            var viewInstance = ViewProvider.RegisterView(_inventoryViewPrefab, ViewId.Inventory);
            
            Container
                .BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle()
                .WithArguments(viewInstance);

            var inventoryView = (InventoryView)viewInstance;
            Container
                .Bind<CellsContainerModel>()
                .WithId(BindingIdentifiers.InventoryCellsContainer)
                .AsCached()
                .WithArguments(inventoryView.CellsParentTransform);
            
            Container
                .BindFactory<CellView, CellView.Factory>()
                .FromComponentInNewPrefab(_cellViewPrefab);
            
            Container
                .BindFactory<CellModel, CellView, CellController, CellController.Factory>()
                .AsSingle();
        }
    }
}
