// using System.Collections.Generic;
// using System.Linq;
// using Core.SaveSystem.Saving.Common.Load;
// using Game.Inventory.Cells.Core.Models;
// using Game.Inventory.Items.Enums;
//
// namespace Game.Inventory.HotBar.Models
// {
//     public partial class HotBarModel
//     {
//         public new class Data
//         {
//             public List<CellModel.Data> CellsData { get; set; }
//         }
//
//         protected override string DataKey => "HotBarModel.Data";
//         
//         protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
//         {
//             switch (loadResult.Result)
//             {
//                 case Result.LoadedSuccessfully:
//                     if (loadResult.Data.CellsData.Count == 0)
//                     {
//                         InitEmptyInventory();
//                     }
//                     break;
//                 
//                 case Result.SaveFileNotFound:
//                     InitEmptyInventory();
//                     break;
//                 
//                 case Result.LoadedWithErrors:
//                     InitEmptyInventory();
//                     break;
//             }
//
//             void InitEmptyInventory()
//             {
//                 loadResult.Data.CellsData = new List<CellModel.Data>();
//                 for (int i = 0; i < InitialHotBarCellsCount; i++)
//                 {
//                     loadResult.Data.CellsData.Add(new CellModel.Data {
//                         ItemId = ItemId.None,
//                         Count = 0,
//                     });
//                 }
//             }
//         }
//
//         protected override void HandleOnDataPreSaved()
//         {
//             var cellsData = HotBarCellsContainerModel.Cells.Select(x => x.GetSaveData());
//             base.Data.CellsData = cellsData.ToList();
//         }
//     }
// }
