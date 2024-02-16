namespace Game.Inventory.Items.Enums.Models
{
    public class InventoryItemModel
    {
        public ItemId ItemId { get; private set; }
        public int Count { get; private set; }
        
        public InventoryItemModel(ItemId itemId, int count)
        {
            ItemId = itemId;
            Count = count;
        }
    }
}
