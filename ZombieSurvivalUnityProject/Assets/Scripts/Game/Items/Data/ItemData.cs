using System;
using UnityEngine;

namespace Game.Items.Data
{
    [Serializable]
    public class ItemData : BaseItemData
    {
        [field: SerializeField] public int Count { get; set; }
    }
}
