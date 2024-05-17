using Game.ItemsDB.Item.Enums;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel
    {
        public class Data
        {
            public ItemId ItemId { get; set; }
            public int Count { get; set; }
        }
        
        public Data GetSaveData()
        {
            return new Data
            {
                ItemId = ContainsItem ? Item.Data.Id : ItemId.None,
                Count = ContainsItem ? Item.Data.Count : 0
            };
        }
    }
}
