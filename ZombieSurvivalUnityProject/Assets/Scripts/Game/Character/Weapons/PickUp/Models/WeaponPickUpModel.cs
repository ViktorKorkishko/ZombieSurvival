using System;
using Game.Weapons.Common.Models;

namespace Game.Character.Weapons.PickUp.Models
{
    public class WeaponPickUpModel
    {
        public Action<WeaponModel> OnWeaponPickedUp { get; set; }

        public void PickUp(WeaponModel weaponModel)
        {
            OnWeaponPickedUp?.Invoke(weaponModel);
        }
    }
}
