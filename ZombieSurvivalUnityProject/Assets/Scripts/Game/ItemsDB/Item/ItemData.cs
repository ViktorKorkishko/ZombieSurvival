using System;
using Game.Inventory.Items.Enums;
using UnityEngine;

namespace Game.ItemsDB.Item
{
    [Serializable]
    public class ItemData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemId _id;

        [SerializeField] private Property[] _properties;

        public ItemId Id => _id;
        public Sprite Sprite => _sprite;
    }
}
