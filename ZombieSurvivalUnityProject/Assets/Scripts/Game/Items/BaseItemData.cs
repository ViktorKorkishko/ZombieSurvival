using System;
using Game.ItemsDB.Item.Enums;
using UnityEngine;

namespace Game.Items
{
    [Serializable]
    public class BaseItemData
    {
        [field: SerializeField] public ItemId Id { get; set; }
    }
}
