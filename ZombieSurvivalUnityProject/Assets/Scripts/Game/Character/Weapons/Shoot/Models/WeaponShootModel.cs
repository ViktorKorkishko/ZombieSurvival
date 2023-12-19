using System;

namespace Game.Character.Weapons.Shoot.Models
{
    public class WeaponShootModel
    {
        public Action OnTryShoot { get; set; }
        
        public void TryShoot()
        {
            OnTryShoot?.Invoke();
        }
    }
}
