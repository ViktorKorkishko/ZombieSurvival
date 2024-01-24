using System;

namespace Game.Character.Weapons.Shoot.Models
{
    public class CharacterWeaponShootModel
    {
        public Action OnTryShoot { get; set; }
        
        public void TryShoot()
        {
            OnTryShoot?.Invoke();
        }
    }
}
