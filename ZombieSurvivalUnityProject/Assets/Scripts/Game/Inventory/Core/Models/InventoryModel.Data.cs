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
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    if (loadResult.Data.CellsData.Count == 0)
                    {
                        InitEmptyInventory();
                    }
                    else
                    {
                        loadResult.Data.CellsData = loadResult.Data.CellsData;
                    }
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
                    loadResult.Data.CellsData.Add(new CellModel.Data {
                        ItemId = ItemId.None,
                        Count = 0,
                    });
                }
            }
        }

        protected override void HandleOnDataPreSaved()
        {
            var cellsData = CellsContainerModel.Cells.Select(x => x.GetSaveData());
            base.Data.CellsData = cellsData.ToList();
        }
    }
}
