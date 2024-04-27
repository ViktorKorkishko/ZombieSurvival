using System;
using UnityEngine;

namespace Game.ItemsDB.Item.Properties.Implementations
{
    public enum WeaponType
    {
        Melee = 0,
        Ranged,
    }

    [Serializable]
    public class WeaponProperty : PropertyBase
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
