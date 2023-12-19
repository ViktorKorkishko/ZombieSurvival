using System;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.PickUp.Models
{
    public class WeaponPickUpModel : MonoBehaviour
    {
        [SerializeField] private float _maxPickUpDistance;
        
        public float MaxPickUpDistance => _maxPickUpDistance;
        
        public Action<DiContainer> OnWeaponPickedUp { get; set; }

        public void PickUp(DiContainer weaponContainer)
        {
            OnWeaponPickedUp?.Invoke(weaponContainer);
        }
    }
}
