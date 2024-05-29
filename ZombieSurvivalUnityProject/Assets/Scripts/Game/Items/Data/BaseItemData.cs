using System;
using System.Linq;
using Game.Items.Enums;
using Game.Items.Properties;
using UnityEngine;

namespace Game.Items.Data
{
    [Serializable]
    public class BaseItemData
    {
        [field: SerializeField] public ItemId Id { get; set; }
        
        // conditional properties
        [SerializeReference] protected PropertyBase[] _properties;

        public BaseItemData()
        {
            Id = ItemId.None;
            _properties = Array.Empty<PropertyBase>();
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
