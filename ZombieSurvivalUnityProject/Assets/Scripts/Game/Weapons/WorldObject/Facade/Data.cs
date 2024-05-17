using System;
using Core.Lifetime.Facade;
using UnityEngine;

namespace Game.Weapons.WorldObject.Facade
{
    [Serializable]
    public class Data : FacadeDataBase
    {
        [field: SerializeField] public int BulletsCount { get; set; }
    }
}
