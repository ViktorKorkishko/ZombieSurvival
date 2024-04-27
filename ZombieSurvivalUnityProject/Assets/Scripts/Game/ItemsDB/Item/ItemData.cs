using System;
using System.Linq;
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
        [SerializeReference] private PropertyBase[] _properties;

        public ItemId Id => _id;
        public Sprite Sprite => _sprite;
        public int MaxStackCount => _maxStackCount;

        public bool HasProperty<T>() where T : PropertyBase
        {
            var property = _properties.FirstOrDefault(x => x.GetType() == typeof(T));
            return property != null;
        }

        public bool TryGetProperty<T>(out PropertyBase propertyInstance) where T : PropertyBase
        {
            propertyInstance = _properties.FirstOrDefault(x => x.GetType() == typeof(T));
            if (propertyInstance != null)
            {
                return true;
            }
            
            return false;
        }
    }
}
