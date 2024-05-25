using Core.Installers;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Core.ViewSystem.Providers.Interfaces;
using Game.Inventory.Cells.CellsContainer.Controllers;
using Game.Inventory.Cells.CellsContainer.Models;
using Game.Inventory.Cells.Core.Controllers;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.HotBar.Controllers;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [Inject] private IViewProvider ViewProvider { get; }

        [Header("View")] 
        [SerializeField] private InventoryView _inventoryViewPrefab;
        [SerializeField] private HotBarView _hotBarViewPrefab;

        [Header("Cells")] 
        [SerializeField] private CellView _cellViewPrefab;
        [SerializeField] private int _initialInventoryCellsCount;
        [SerializeField] private int _initialHotBarCellsCount;

        public override void InstallBindings()
        {
            Container
                .BindFactory<CellView, CellView.Factory>()
                .FromComponentInNewPrefab(_cellViewPrefab);

            Container
                .BindFactory<CellModel, CellView, CellController, CellController.Factory>()
                .AsSingle();

            #region Inventory
            
            Container
                .BindInterfacesAndSelfTo<InventoryModel>()
                .AsSingle()
                .WithArguments(_initialInventoryCellsCount);

            var inventoryViewInstance = ViewProvider.RegisterView(_inventoryViewPrefab, ViewId.Inventory, LayerId.Windows);
            
            #endregion
            
            BindInventoryCellsContainer();
            BindInventoryHotBarCellsContainer();
            BindHotBar();
            
            Container
                .BindInterfacesTo<InventoryController>()
                .AsSingle()
                .WithArguments(inventoryViewInstance);

            void BindInventoryCellsContainer()
            {
                var inventoryView = (InventoryView)inventoryViewInstance;
                var cellsContainerModel = Container.Instantiate<CellsContainerModel>();
                Container
                    .Bind<CellsContainerModel>()
                    .WithId(BindingIdentifiers.InventoryCellsContainer)
                    .FromInstance(cellsContainerModel)
                    .AsCached();
                
                Container
                    .BindInterfacesAndSelfTo<CellsContainerController>()
                    .AsCached()
                    .WithArguments(cellsContainerModel, inventoryView.InventoryCellsContainerView);
            }
            
            void BindInventoryHotBarCellsContainer()
            {
                var inventoryView = (InventoryView)inventoryViewInstance;
                var cellsContainerModel = Container.Instantiate<CellsContainerModel>();
                Container
                    .Bind<CellsContainerModel>()
                    .WithId(BindingIdentifiers.InventoryHotBarCellsContainer)
                    .FromInstance(cellsContainerModel)
                    .AsCached();
                
                Container
                    .BindInterfacesAndSelfTo<CellsContainerController>()
                    .AsCached()
                    .WithArguments(cellsContainerModel, inventoryView.HotBarCellsContainerView);
            }
            
            void BindHotBar()
            {
                #region HotBar
                
                Container
                    .BindInterfacesAndSelfTo<HotBarModel>()
                    .AsSingle()
                    .WithArguments(_initialHotBarCellsCount);
                
                var hotBarViewInstance = ViewProvider.RegisterView(_hotBarViewPrefab, ViewId.HotBar, LayerId.HUD);
                hotBarViewInstance.Show();
                
                Container
                    .BindInterfacesTo<HotBarController>()
                    .AsSingle()
                    .WithArguments(hotBarViewInstance);
                
                #endregion
                
                #region HotBar Cells Container
                
                var cellsContainerModel = Container.Instantiate<CellsContainerModel>();
                Container
                    .Bind<CellsContainerModel>()
                    .WithId(BindingIdentifiers.HotBarCellsContainer)
                    .FromInstance(cellsContainerModel)
                    .AsCached();
                
                var hotBarView = (HotBarView)hotBarViewInstance;
                Container
                    .BindInterfacesAndSelfTo<CellsContainerController>()
                    .AsCached()
                    .WithArguments(cellsContainerModel, hotBarView.CellsContainerView);

                #endregion
            }
        }
    }
}
