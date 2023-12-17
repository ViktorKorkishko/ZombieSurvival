using System;
using Game.Weapons.Common.Models;
using UnityEngine;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public class CurrentWeaponModel : MonoBehaviour
    {
        public WeaponModel Weapon { get; private set; }
        
        public Action<WeaponModel> OnCurrentWeaponSet { get; set; }

        public void SetCurrentWeapon(WeaponModel weaponModel)
        {
            OnCurrentWeaponSet?.Invoke(weaponModel);
            Weapon = weaponModel;
        }
    }
}
