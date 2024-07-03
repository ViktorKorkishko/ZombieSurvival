using System.Collections.Generic;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Models;
using Core.ViewSystem.Providers.Interfaces;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.HotBar.Models;
using UnityEngine;
using Zenject;

namespace Game._TEMP_
{
    public class Hotkeys : MonoBehaviour
    {
        [Inject] private HotBarModel HotBarModel { get; }
        [Inject] private ViewSystemModel ViewSystemModel { get; }
        [Inject] private IViewProvider ViewProvider { get; }
        
        private List<CellModel> Cells => HotBarModel.HotBarCellsContainerModel.Cells;
        
        private void Update()
        {
            Hotbar();
            Inventory();
            Settings();
        }

        private void Hotbar()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Cells[0].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Cells[1].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Cells[2].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Cells[3].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Cells[4].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Cells[5].SetSelected(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Cells[6].SetSelected(true);
            }
        }
        
        private void Inventory()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ViewSystemModel.Show(ViewId.Inventory);
            }
        }
        
        private void Settings()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewSystemModel.Show(ViewId.Settings);
            }
        }
    }
}
