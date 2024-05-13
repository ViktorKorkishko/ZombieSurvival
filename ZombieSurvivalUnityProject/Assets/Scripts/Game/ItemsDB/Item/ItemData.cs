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
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public ItemId Id { get; private set; }
        [field: SerializeField] public int MaxStackCount { get; private set; }
        
        // conditional properties
        [SerializeReference] private PropertyBase[] _properties;
        
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
