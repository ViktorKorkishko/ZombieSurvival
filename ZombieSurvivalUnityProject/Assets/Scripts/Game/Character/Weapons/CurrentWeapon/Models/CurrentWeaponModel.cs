using System;
using Zenject;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public class CurrentWeaponModel
    {
        public DiContainer WeaponContainer { get; private set; }
        public bool IsWeaponEquipped => WeaponContainer != null;
        
        public Action<DiContainer> OnWeaponSet { get; set; }

        public void SetCurrentWeapon(DiContainer weaponContainer)
        {
            WeaponContainer = weaponContainer;
            OnWeaponSet?.Invoke(weaponContainer);
        }
    }
}
