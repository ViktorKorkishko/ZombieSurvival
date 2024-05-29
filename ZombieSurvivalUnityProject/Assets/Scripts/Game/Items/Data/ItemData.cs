using System;
using Game.Items.Enums;
using Game.Items.Properties;
using UnityEngine;

namespace Game.Items.Data
{
    [Serializable]
    public class ItemData : BaseItemData, ICloneable
    {
        [field: SerializeField] public int Count { get; set; }

        public ItemData()
        {
            Count = 0;
        }
        
        public ItemData(ItemId itemId, int count, PropertyBase[] properties)
        {
            Id = itemId;
            Count = count;
            _properties = properties;
        }
        
        public object Clone()
        {
            return new ItemData(Id, Count, (PropertyBase[])_properties.Clone());
        }
    }
}
