using System.Collections.Generic;
using System.Linq;
using Core.SaveSystem.Saving.Common.Load;
using Game.Inventory.Cells.Core.Models;
using Game.Inventory.Items.Enums;

namespace Game.Inventory.Core.Models
{
    public partial class InventoryModel
    {
        public new class Data
        {
            public List<CellModel.Data> InventoryCellsData { get; set; }
            public List<CellModel.Data> HotBarCellsData { get; set; }
        }
        
        protected override string DataKey => "InventoryModel.Data";
        
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            var data = loadResult.Data;
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    if (data.InventoryCellsData.Count == 0 || 
                        data.HotBarCellsData.Count == 0)
                    {
                        InitEmptyCells(data.InventoryCellsData, InitialInventoryCellsCount);
                        InitEmptyCells(data.HotBarCellsData, 7);
                    }
                    break;
                
                case Result.SaveFileNotFound:
                    InitEmptyCells(data.InventoryCellsData, InitialInventoryCellsCount);
                    InitEmptyCells(data.HotBarCellsData, 7);
                    break;
                
                case Result.LoadedWithErrors:
                    InitEmptyCells(data.InventoryCellsData, InitialInventoryCellsCount);
                    InitEmptyCells(data.HotBarCellsData, 7);
                    break;
            }

            void InitEmptyCells(List<CellModel.Data> cellsData, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    cellsData.Add(new CellModel.Data {
                        ItemId = ItemId.None,
                        Count = 0,
                    });
                }
            }
        }

        protected override void HandleOnDataPreSaved()
        {
            var inventoryCellsData = InventoryCellsContainerModel.Cells.Select(x => x.GetSaveData());
            base.Data.InventoryCellsData = inventoryCellsData.ToList();
            
            var hotBarCellsData = InventoryHotBarCellsContainer.Cells.Select(x => x.GetSaveData());
            base.Data.HotBarCellsData = hotBarCellsData.ToList();
        }
    }
}
