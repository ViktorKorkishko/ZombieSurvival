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
            public List<CellModel.Data> CellsData { get; set; }
        }
        
        protected override string DataKey => "InventoryModel.Data";
        
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            var cellsData = new List<CellModel.Data>();
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    cellsData = loadResult.Data.CellsData;
                    InitCellsData(cellsData);
                    break;
                
                case Result.SaveFileNotFound:
                    InitEmptyInventory();
                    break;
                
                case Result.LoadedWithErrors:
                    InitEmptyInventory();
                    break;
            }

            void InitEmptyInventory()
            {
                for (int i = 0; i < InitialInventoryCellsCount; i++)
                {
                    cellsData.Add(new CellModel.Data {
                        ItemId = ItemId.None,
                        Count = 0,
                    });
                }
                
                InitCellsData(cellsData);
            }

            void InitCellsData(List<CellModel.Data> data)
            {
                CellsContainerModel.InitCells(data);
            }
        }

        protected override void HandleOnDataPreSaved()
        {
            var cellsData = CellsContainerModel.Cells.Select(x => x.GetSaveData());
            base.Data.CellsData = cellsData.ToList();
        }
    }
}
