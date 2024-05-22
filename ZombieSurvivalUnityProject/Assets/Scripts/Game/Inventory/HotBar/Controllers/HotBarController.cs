using System;
using Core.Lifetime.Instantiation;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Common.SelectableCollection;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using Game.Items.Database;
using Game.Items.Properties.Implementations;
using Game.Weapons.Facade;
using Zenject;

namespace Game.Inventory.HotBar.Controllers
{
    public class HotBarController : IInitializable, IDisposable
    {
        [Inject] private HotBarModel HotBarModel { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private CharacterWeaponPickUpModel CharacterWeaponPickUpModel { get; }
        [Inject] private CharacterWeaponEquipModel CharacterWeaponEquipModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private Instantiator Instantiator { get; }

        private HotBarView View { get; }

        private SelectableCollection<CellModel> _cellsSelectableCollection;
        private SynchronizableCellsContainer _synchronizableCellsContainer;

        public HotBarController(HotBarView view)
        {
            View = view;
        }

        void IInitializable.Initialize()
        {
            View.Show();

            _synchronizableCellsContainer = new SynchronizableCellsContainer(
                HotBarModel.InventoryHotBarCellsContainer,
                HotBarModel.HotBarCellsContainerModel);
            
            _synchronizableCellsContainer.Initialize();
            
            if (HotBarModel.InventoryHotBarCellsContainer.IsInited)
            {
                HandleOnHotBarCellsInitialized();
            }
            else
            {
                HotBarModel.InventoryHotBarCellsContainer.OnInitialized += HandleOnHotBarCellsInitialized;
            }
        }

        void IDisposable.Dispose()
        {
            _cellsSelectableCollection.OnSelectedCellChanged -= HandleOnSelectedCellChanged;
            
            ((IDisposable)_cellsSelectableCollection)?.Dispose();
            ((IDisposable)_synchronizableCellsContainer)?.Dispose();
        }
        
        private void HandleOnHotBarCellsInitialized()
        {
            HotBarModel.InventoryHotBarCellsContainer.OnInitialized -= HandleOnHotBarCellsInitialized;
            
            _cellsSelectableCollection = new SelectableCollection<CellModel>(HotBarModel.HotBarCellsContainerModel.Cells);
            _cellsSelectableCollection.OnSelectedCellChanged += HandleOnSelectedCellChanged;
            
            _cellsSelectableCollection.Initialize();
        }
        
        private void HandleOnSelectedCellChanged(CellModel cellModel)
        {
            if (cellModel.ContainsItem)
            {
                var itemId = cellModel.ItemId;
                if (ItemsDataBase.TryGetItemData(itemId, out var itemData))
                {
                    if (itemData.TryGetProperty<WeaponProperty>(out var weaponProperty))
                    {
                        HandleWeapon(weaponProperty as WeaponProperty);
                    }
                }
            }
            else
            {
                if (CurrentWeaponModel.IsWeaponEquipped)
                {
                    CharacterWeaponEquipModel.Unequip();
                }
            }

            void HandleWeapon(WeaponProperty weaponProperty)
            {
                if (CurrentWeaponModel.IsWeaponEquipped)
                {
                    CharacterWeaponEquipModel.Unequip();
                }

                var weaponPrefab = weaponProperty.ItemPrefab;
                var weaponGameObject = Instantiator.InstantiatePrefabForComponent<WeaponFacade>(weaponPrefab);
                CharacterWeaponPickUpModel.PickUp(weaponGameObject);
            }
        }
    }
}
