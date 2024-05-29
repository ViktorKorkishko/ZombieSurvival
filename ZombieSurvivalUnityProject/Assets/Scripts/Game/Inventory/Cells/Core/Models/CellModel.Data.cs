using Game.Items.Data;

namespace Game.Inventory.Cells.Core.Models
{
    public partial class CellModel
    {
        public class Data
        {
            public ItemData ItemData { get; set; }

            public Data()
            {
                ItemData = new();
            }
        }
        
        public Data GetSaveData()
        {
            return new Data
            {
                ItemData = ContainsItem ? _item.Data : new(),
            };
        }
    }
}
