using System;
using System.Linq;
using Game.ItemsDB.Item;
using Game.ItemsDB.Item.Enums;
using UnityEngine;

namespace Game.Items.Database
{
    [CreateAssetMenu(menuName = "Game/DB/ItemsDataBase", fileName = "ItemsDataBase", order = 0)]
    public class ItemsDataBase : ScriptableObject
    {
        [SerializeField] private DBBaseItemData[] _items;

        public bool TryGetItemData(ItemId id, out DBBaseItemData dbBaseItemData)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                dbBaseItemData = item;
                return true;
            }

            Debug.LogException(new NullReferenceException($"Item with id [{id}] is not found!"));
            dbBaseItemData = null;
            return false;
        }
    }
}
