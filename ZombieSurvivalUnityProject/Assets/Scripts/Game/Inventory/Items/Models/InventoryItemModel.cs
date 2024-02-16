using Game.Inventory.Items.Enums;
using Game.ItemsDB;
using Game.ItemsDB.Item;

namespace Game.Inventory.Items.Models
{
    public class InventoryItemModel
    {
        public ItemId ItemId { get; private set; }
        public int Count { get; private set; }
        public ItemData ItemData => GetItemData();

        private ItemsDataBase ItemsDataBase { get; }

        public InventoryItemModel(ItemId itemId, int count, ItemsDataBase itemsDataBase)
        {
            ItemId = itemId;
            Count = count;
            
            ItemsDataBase = itemsDataBase;
        }

        private ItemData GetItemData()
        {
            ItemsDataBase.TryGetItemData(ItemId, out var itemData);
            return itemData;
        }
    }
}
