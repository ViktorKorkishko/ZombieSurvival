using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using Game.Inventory.Core.Controllers;
using Game.Inventory.DragAndDrop.Models;
using Game.ItemsDB;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Inventory.DragAndDrop.Controllers
{
    // callbacks execution order
    // 1. HandleOnBeginDrag
    // 2. HandleOnDrag
    // 3. HandleOnDrop
    // 4. HandleOnEndDrag

    // TODO: refactor (add dependencies have to be injected)
    public partial class DragAndDropController : IInitializable, IDisposable
    {
        [Inject] private DragAndDropModel DragAndDropModel { get; }
        [Inject] private ItemsDataBase ItemsDataBase { get; }
        
        private Transform DraggableObjectRoot { get; }
        private Dictionary<CellView, CellModel> _cellViewToModelDictionary { get; } = new();

        private DragData _dragData;
        
        public DragAndDropController(Transform draggableObjectRoot)
        {
            DraggableObjectRoot = draggableObjectRoot;
        }

        void IInitializable.Initialize()
        {
            DragAndDropModel.OnCellRegistered += HandleOnCellRegistered;
            DragAndDropModel.OnCellUnregistered += HandleOnCellUnregistered;
        }

        void IDisposable.Dispose()
        {
            DragAndDropModel.OnCellRegistered -= HandleOnCellRegistered;
            DragAndDropModel.OnCellUnregistered -= HandleOnCellUnregistered;
        }

        private void RegisterCell(CellView cellView, CellModel cellModel)
        {
            cellView.OnBeginDrag += HandleOnBeginDrag;
            cellView.OnEndDrag += HandleOnEndDrag;
            cellView.OnDrag += HandleOnDrag;
            cellView.OnDrop += HandleOnDrop;
            
            _cellViewToModelDictionary.Add(cellView, cellModel);
        }
        
        private void UnregisterCell(CellView cellView)
        {
            cellView.OnBeginDrag -= HandleOnBeginDrag;
            cellView.OnEndDrag -= HandleOnEndDrag;
            cellView.OnDrag -= HandleOnDrag;
            cellView.OnDrop -= HandleOnDrop;
            
            _cellViewToModelDictionary.Remove(cellView);
        }

        private void HandleOnCellRegistered(CellView cellView, CellModel cellModel)
        {
            RegisterCell(cellView, cellModel);
        }

        private void HandleOnCellUnregistered(CellView cellView)
        {
            UnregisterCell(cellView);
        }
    }
}
