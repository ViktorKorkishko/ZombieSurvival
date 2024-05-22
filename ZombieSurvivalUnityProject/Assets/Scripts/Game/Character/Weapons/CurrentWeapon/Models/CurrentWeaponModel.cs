using System;
using Game.Weapons.Facade;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public class CurrentWeaponModel
    {
        public WeaponFacade Weapon { get; private set; }
        public bool IsWeaponEquipped => Weapon != null;
        
        public Action<WeaponFacade> OnWeaponSet { get; set; }

        public void SetCurrentWeapon(WeaponFacade weaponFacade)
        {
            Weapon = weaponFacade;
            OnWeaponSet?.Invoke(weaponFacade);
        }
    }
}
