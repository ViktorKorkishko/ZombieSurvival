using System;

namespace Game.Character.Weapons.Reload.Models
{
    public class CharacterWeaponReloadModel
    {
        public Action OnTryReload { get; set; }

        public void TryReload()
        {
            OnTryReload?.Invoke();
        }
    }
}