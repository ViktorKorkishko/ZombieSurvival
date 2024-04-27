using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inventory.Cells;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.HotBar.Models;
using Game.Inventory.HotBar.Views;
using Game.ItemsDB;
using Game.ItemsDB.Item.Properties.Implementations;
using Zenject;

namespace Game.Inventory.HotBar.Controllers
{
    public class HotBarController : IInitializable
    {
        [Inject] private HotBarModel HotBarModel { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private CharacterWeaponPickUpModel CharacterWeaponPickUpModel { get; }

        private HotBarView View { get; }

        private CellSelectionService _cellSelectionService;
        private SynchronizableCellsContainer _synchronizableCellsContainer;

        public HotBarController(HotBarView view)
        {
            View = view;
        }

        void IInitializable.Initialize()
        {
            // View.Show();

            // _synchronizableCellsContainer = new SynchronizableCellsContainer(
            //     HotBarModel.InventoryHotBarCellsContainer,
            //     HotBarModel.HotBarCellsContainerModel);
            //
            // _synchronizableCellsContainer.Initialize();
            //
            // if (HotBarModel.InventoryHotBarCellsContainer.IsInited)
            // {
            //     HandleOnHotBarCellsInitialized();
            // }
            // else
            // {
            //     HotBarModel.InventoryHotBarCellsContainer.OnInitialized += HandleOnHotBarCellsInitialized;
            // }
        }
        
        private void HandleOnHotBarCellsInitialized()
        {
            HotBarModel.InventoryHotBarCellsContainer.OnInitialized -= HandleOnHotBarCellsInitialized;
            
            _cellSelectionService = new CellSelectionService(HotBarModel.HotBarCellsContainerModel.Cells);
            _cellSelectionService.OnSelectedCellChanged += HandleOnSelectedCellChanged;
            
            _cellSelectionService.Initialize();
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

            void HandleWeapon(WeaponProperty weaponProperty)
            {
                var weaponPrefab = weaponProperty.Prefab;

                var context = weaponPrefab.GetComponentInChildren<Context>();
                var container = context.Container;
                var equippedWeapon = new EquippedWeapon(container);
                
                CharacterWeaponPickUpModel.PickUp(equippedWeapon);
            }
        }
    }
}
