using System;
using Core.SaveSystem.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Weapons.Facade;

namespace Game.Character.Weapons.CurrentWeapon.Models
{
    public partial class CurrentWeaponModel : SaveableModel<CurrentWeaponModel.Data>
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
