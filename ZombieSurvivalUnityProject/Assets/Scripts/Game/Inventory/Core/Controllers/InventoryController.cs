using System;
using System.Collections.Generic;
using System.Linq;
using Core.Installers;
using Core.Lifetime.Instantiation;
using Core.ViewSystem.Views.Interfaces;
using Game.Common.SelectableCollection;
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
    public class InventoryController : IInitializable, IDisposable
    {
        [Inject] private InventoryModel InventoryModel { get; }
        [Inject] private DragAndDropModel DragAndDropModel { get; }
        [Inject] private Instantiator Instantiator { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private WorldObjectsModel WorldObjectsModel { get; }
        [Inject(Id = BindingIdentifiers.ViewRoot)] private Transform CharacterViewRoot { get; }

        private InventoryView InventoryView { get; }
        
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

        public InventoryController(InventoryView inventoryView)
        {
            InventoryView = inventoryView;
        }
        
        void IInitializable.Initialize()
        {
            InventoryModel.OnItemsAdded += HandleOnItemsAdded;

            InventoryView.OnShow += HandleOnShow;
            InventoryView.OnHide += HandleOnHide;
            InventoryView.OnDeleteItemButtonClicked += HandleOnDeleteItemButtonClicked;
            InventoryView.OnDropItemButtonClicked += HandleOnDropItemButtonClicked;
            
            InventoryModel.InitializeCells();
            
            _cellsSelectableCollection = new SelectableCollection<CellModel>(Cells);
            _cellsSelectableCollection.OnSelectedChanged += HandleOnSelectedCellChanged;

            foreach (var cell in Cells)
            {
                cell.OnItemRemoved += HandleInItemRemoved;
            }
            
            _cellsSelectableCollection.Initialize();
        }

        private void HandleInItemRemoved(CellModel cell)
        {
            if (!cell.IsSelected)
                return;
            
            InventoryView.SetDeleteButtonEnabled(cell.ContainsItem);
            InventoryView.SetDropButtonEnabled(cell.ContainsItem);
        }

        void IDisposable.Dispose()
        {
            InventoryModel.OnItemsAdded -= HandleOnItemsAdded;

            InventoryView.OnShow -= HandleOnShow;
            InventoryView.OnHide -= HandleOnHide;
            InventoryView.OnDeleteItemButtonClicked -= HandleOnDeleteItemButtonClicked;
            InventoryView.OnDropItemButtonClicked -= HandleOnDropItemButtonClicked;
            
            _cellsSelectableCollection.OnSelectedChanged -= HandleOnSelectedCellChanged;
            
            foreach (var cell in Cells)
            {
                cell.OnItemRemoved -= HandleInItemRemoved;
            }
        }
        
        private void HandleOnItemsAdded(IEnumerable<InventoryItemModel> items)
        {
            InventoryModel.InventoryCellsContainerModel.SpreadItemsAmongCells(items);
        }
        
        private void HandleOnShow()
        {
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.RegisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);
        }
        
        private void HandleOnHide(IView view)
        {
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryCellsContainerModel);
            DragAndDropModel.UnregisterDraggableCells(InventoryModel.InventoryHotBarCellsContainer);
        }
        
        private void HandleOnDeleteItemButtonClicked()
        {
            if (!_cellsSelectableCollection.SelectedElement.ContainsItem)
                return;

            _cellsSelectableCollection.SelectedElement.RemoveItem();
        }

        private void HandleOnSelectedCellChanged(CellModel cellModel)
        {
            InventoryView.SetDeleteButtonEnabled(cellModel.ContainsItem);
            InventoryView.SetDropButtonEnabled(cellModel.ContainsItem);
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
