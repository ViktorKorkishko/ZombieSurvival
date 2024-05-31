using System;
using System.Linq;
using Core.Lifetime.Instantiation;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Common.SelectableCollection;
using Game.Common.SelectableCollection.Interfaces;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using Game.Inventory.Items.Models;
using Game.Items.Data;
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
            var cells = HotBarModel.HotBarCellsContainerModel.Cells;
            foreach (var cell in cells)
            {
                cell.OnItemSet -= HandleOnItemSet;
            }
            
            _cellsSelectableCollection.OnSelectedCellChanged -= HandleOnSelectedCellChanged;
            
            ((IDisposable)_cellsSelectableCollection)?.Dispose();
            ((IDisposable)_synchronizableCellsContainer)?.Dispose();
        }
        
        private void HandleOnHotBarCellsInitialized()
        {
            HotBarModel.InventoryHotBarCellsContainer.OnInitialized -= HandleOnHotBarCellsInitialized;

            var cells = HotBarModel.HotBarCellsContainerModel.Cells;
            foreach (var cell in cells)
            {
                cell.OnItemSet += HandleOnItemSet;
                cell.OnItemRemoved += HandleOnItemRemoved;
            }
            
            _cellsSelectableCollection = new SelectableCollection<CellModel>(cells);
            _cellsSelectableCollection.OnSelectedCellChanged += HandleOnSelectedCellChanged;
            
            _cellsSelectableCollection.Initialize();
        }

        private void HandleOnSelectedCellChanged(CellModel cellModel)
        {
            if (cellModel.ContainsItem)
            {
                HandleItemInCell(cellModel);
            }
            else
            {
                if (CurrentWeaponModel.IsWeaponEquipped) 
                { 
                    CharacterWeaponEquipModel.Unequip();
                }
            }
        }
        
        private void HandleOnItemSet(CellModel cellModel, InventoryItemModel itemModel)
        {
            if (!cellModel.IsSelected)
                return;
            
            HandleItemInCell(cellModel);
        }

        private void HandleOnItemRemoved(CellModel cellModel)
        {
            if (!cellModel.IsSelected)
                return;
            
            if (CurrentWeaponModel.IsWeaponEquipped) 
            { 
                CharacterWeaponEquipModel.Unequip();
            }
        }

        private void HandleItemInCell(CellModel cellModel)
        {
            var itemId = cellModel.ItemId;
            if (ItemsDataBase.TryGetItemData(itemId, out var itemData))
            {
                if (itemData.TryGetProperty<PickableItemProperty>(out var pickableItemProperty))
                {
                    HandleWeapon(cellModel.ItemData, pickableItemProperty as PickableItemProperty);
                }
            }
        }

        private void HandleWeapon(ItemData itemData, PickableItemProperty pickableItemProperty)
        {
            if (CurrentWeaponModel.IsWeaponEquipped)
            {
                CharacterWeaponEquipModel.Unequip();
            }

            var weaponPrefab = pickableItemProperty.ItemPrefab;
            var weaponFacade = Instantiator.InstantiatePrefabForComponent<WeaponFacade>(weaponPrefab);
            CharacterWeaponPickUpModel.PickUp(weaponFacade);
            weaponFacade.Init(itemData);
        }
    }
}
