using System;
using Game.Weapons.Common.Models;
using UnityEngine;

namespace Game.Weapons.Common.Views
{
    public class WeaponView : MonoBehaviour
    {
        public Func<WeaponModel> OnWeaponPickedUp { get; set; }

        public WeaponModel PickUp()
        {
            return OnWeaponPickedUp?.Invoke();
        }
    }
}