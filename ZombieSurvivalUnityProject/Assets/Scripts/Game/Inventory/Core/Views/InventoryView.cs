using System;
using System.Collections.Generic;
using Core.ViewSystem.Views;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Cells.Core.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Inventory.Core.Views
{
    public class InventoryView : ViewBase
    {
        [SerializeField] private CellView _cellViewPrefab;
        [SerializeField] private Transform _cellsParentTransform;

        private List<CellView> _cellViews = new();

        public Action<CellView, PointerEventData> OnHandleDown { get; set; }

        public CellView InitCell(CellModel cell)
        {
            CellView newCellView = Instantiate(_cellViewPrefab, _cellsParentTransform);
            newCellView.name = _cellViewPrefab.name + $"_{_cellViews.Count}";
            newCellView.OnDown += HandleDown;
            _cellViews.Add(newCellView);
            return newCellView;
        }

        private void HandleDown(CellView cellView, PointerEventData obj)
        {
            OnHandleDown?.Invoke(cellView, obj);
        }
    }
}
