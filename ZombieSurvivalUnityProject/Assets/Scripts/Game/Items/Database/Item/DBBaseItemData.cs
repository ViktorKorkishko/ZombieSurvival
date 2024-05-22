using System;
using Game.Items.Data;
using UnityEngine;

namespace Game.Items.Database.Item
{
    [Serializable]
    public class DBBaseItemData : BaseItemData
    {
        // shared properties
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int MaxStackCount { get; private set; }
    }
}
