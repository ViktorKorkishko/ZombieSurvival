using System;
using UnityEngine;

namespace Game.Items.Properties.Implementations
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
        [field: SerializeField] public GameObject ItemPrefab { get; private set; }
        [field: SerializeField] public GameObject WorldObjectPrefab { get; private set; }
    }
}
