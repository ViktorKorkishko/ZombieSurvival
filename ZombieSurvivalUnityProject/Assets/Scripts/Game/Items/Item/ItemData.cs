using System;
using System.Linq;
using Game.Items;
using Game.ItemsDB.Item.Properties;
using UnityEngine;

namespace Game.ItemsDB.Item
{
    [Serializable]
    public class DBBaseItemData : BaseItemData
    {
        // shared properties
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int MaxStackCount { get; private set; }
        
        // conditional properties
        [SerializeReference] private PropertyBase[] _properties;
        
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
