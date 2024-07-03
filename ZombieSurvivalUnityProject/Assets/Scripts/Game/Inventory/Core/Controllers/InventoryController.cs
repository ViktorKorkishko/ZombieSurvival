using System;
using System.Collections.Generic;
using System.Linq;
using Core.Installers;
using Core.Lifetime.Instantiation;
using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Common.SelectableCollection;
using Game.Hotkeys;
using Game.Hotkeys.Models;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Core.Models;
using Game.Inventory.Core.Views;
using Game.Inventory.DragAndDrop.Models;
using Game.Inventory.Items.Models;
using Game.Items.Database;
using Game.Items.Properties.Implementations;
using Game.WorldObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Controllers
{
    public class InventoryController : ViewControllerBase<InventoryView>
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private DragAndDropModel DragAndDropModel { get; }
        [Inject] private Instantiator Instantiator { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private WorldObjectsModel WorldObjectsModel { get; }
        [Inject] private HotKeysModel HotKeysModel { get; }
        
        [Inject(Id = BindingIdentifiers.ViewRoot)]
        private Transform CharacterViewRoot { get; }
        
        private IEnumerable<CellModel> Cells
        {
            get
            {
                var inventoryCells = InventoryModel.InventoryCellsContainerModel.Cells;
                var hotBarCells = InventoryModel.InventoryHotBarCellsContainer.Cells;
                var cells = inventoryCells.Union(hotBarCells);
                return cells;
            }
        }
        
        private SelectableCollection<CellModel> _cellsSelectableCollection;
        
        public InventoryController(IView view) : base(view) { }
        
        public override void Initialize()
        {
            base.Initialize();
            
            InventoryModel.OnItemsAdded += HandleOnItemsAdded;

            View.OnDeleteItemButtonClicked += HandleOnDeleteItemButtonClicked;
            View.OnDropItemButtonClicked += HandleOnDropItemButtonClicked;

            InventoryModel.InitializeCells();

            _cellsSelectableCollection = new SelectableCollection<CellModel>(Cells);
            _cellsSelectableCollection.OnSelectedChanged += HandleOnSelectedCellChanged;

            foreach (var cell in Cells)
            {
                cell.OnItemSet += HandleOnItemSet;
                cell.OnItemRemoved += HandleOnItemRemoved;
            }

            _cellsSelectableCollection.Initialize();
            
            HotKeysModel.OverrideHotKey(KeyCode.I, () => View.Show(), this);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;

            View.OnDeleteItemButtonClicked -= HandleOnDeleteItemButtonClicked;
            View.OnDropItemButtonClicked -= HandleOnDropItemButtonClicked;

            _cellsSelectableCollection.OnSelectedChanged -= HandleOnSelectedCellChanged;

            foreach (var cell in Cells)
            {
                cell.OnItemSet -= HandleOnItemSet;
                cell.OnItemRemoved -= HandleOnItemRemoved;
            }
            
            HotKeysModel.ClearHotKey(KeyCode.I, this);
        }

        protected override void HandleOnShow()
        {
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);

            var selectedCell = _cellsSelectableCollection.SelectedElement;
            UpdateInventoryButtons(selectedCell);
            
            HotKeysModel.OverrideHotKey(KeyCode.Escape, View.Hide, this);
        }

        protected override void HandleOnHide(IView view)
        {
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);
            
            HotKeysModel.ClearHotKey(KeyCode.Escape, this);
        }

        private void UpdateInventoryButtons(CellModel cell)
        {
            var containsItem = cell.ContainsItem;
            View.SetDeleteButtonEnabled(containsItem);
            if (containsItem)
            {
                if (ItemsDataBase.TryGetItemData(cell.ItemId, out var dbBaseItemData))
                {
                    dbBaseItemData.TryGetProperty<PickableItemProperty>(out var pickableItemProperty);
                    bool droppableItem = pickableItemProperty != null;
                    View.SetDropButtonEnabled(droppableItem);
                }
            }
            else
            {
                View.SetDropButtonEnabled(false);
            }
        }
        
        private void HandleOnItemSet(CellModel cell, InventoryItemModel item)
        {
            if (!cell.IsSelected)
                return;

            UpdateInventoryButtons(cell);
        }
        
        private void HandleOnItemRemoved(CellModel cell)
        {
            if (!cell.IsSelected)
                return;

            UpdateInventoryButtons(cell);
        }
        
        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            InventoryModel.InventoryCellsContainerModel.SpreadItemsAmongCells(items);
        }
        
        private void HandleOnDeleteItemButtonClicked()
        {
            if (!_cellsSelectableCollection.SelectedElement.ContainsItem)
                return;

            _cellsSelectableCollection.SelectedElement.RemoveItem();
        }
        
        private void HandleOnSelectedCellChanged(CellModel cell)
        {
            UpdateInventoryButtons(cell);
        }
        
        private void HandleOnDropItemButtonClicked()
        {
            if (!_cellsSelectableCollection.SelectedElement.ContainsItem)
                return;

            var itemId = _cellsSelectableCollection.SelectedElement.ItemId;
            if (ItemsDataBase.TryGetItemData(itemId, out var dbBaseItemData))
            {
                if (dbBaseItemData.TryGetProperty<PickableItemProperty>(out var pickableItemProperty))
                {
                    var position = CharacterViewRoot.transform.position;
                    var rotation = CharacterViewRoot.transform.rotation;
                    var worldObjectModel = Instantiator.InstantiatePrefabForComponent<WorldObjectModel>(
                        pickableItemProperty.WorldObjectPrefab,
                        position,
                        rotation);
                    
                    var inventoryItem = _cellsSelectableCollection.SelectedElement.RemoveItem();
                    worldObjectModel.Init(new WorldObjectModel.BaseWorldObjectData
                    {
                        Position = position,
                        Rotation = rotation,
                        ItemData = inventoryItem.Data,
                    });
                    
                    WorldObjectsModel.Register(worldObjectModel);
                }
            }
        }
    }
}
