using Game.Inventory.Items.Enums;

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
                ItemId = ContainsItem ? ItemId : ItemId.None,
                Count = ContainsItem ? ItemCount : 0
            };
        }
    }
}
