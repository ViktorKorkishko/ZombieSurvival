using System;
using Game.Inventory.Items.Enums;
using Game.ItemsDB.Item.Properties;
using UnityEngine;

namespace Game.ItemsDB.Item
{
    [Serializable]
    public class ItemData
    {
        // shared properties
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemId _id;
        [SerializeField] private int _maxStackCount;
        
        // conditional properties
        [SerializeField] private Property[] _properties;

        public ItemId Id => _id;
        public Sprite Sprite => _sprite;
        public int MaxStackCount => _maxStackCount;
    }
}
