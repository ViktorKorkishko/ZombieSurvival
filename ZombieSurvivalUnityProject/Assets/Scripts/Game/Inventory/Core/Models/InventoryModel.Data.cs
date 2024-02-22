using System.Collections.Generic;
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
                    Items = loadResult.Data.CellsData;
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
                for (int i = 0; i < InventoryCellsCount; i++)
                {
                    Items.Add(new CellModel.Data {
                        ItemId = ItemId.None,
                        Count = 0,
                    });
                }
            }
        }

        protected override void HandleOnDataPreSaved()
        {
            base.Data.CellsData = Items;
        }
    }
}
