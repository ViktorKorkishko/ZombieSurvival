using System.Linq;
using Game.Inventory.Items.Enums;
using Game.ItemsDB.Item;
using TriInspector;
using UnityEngine;

namespace Game.ItemsDB
{
    [DeclareTabGroup(Tabs.TabGroup)]
    public partial class ItemsDataBase
    {
        [Group(Tabs.TabGroup), Tab(Tabs.ItemsTabHeader)] 
        [SerializeField] private ItemData[] _items;

        public bool TryGetItemData(ItemId id, out ItemData itemData)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            itemData = item;
            return item != null;
        }
    }
}
