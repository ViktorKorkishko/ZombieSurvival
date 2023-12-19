using System;

namespace Game.Weapons.Reload.Models
{
    public class WeaponReloadModel
    {
        public bool IsRealoding { get; private set; }
        
        public Action OnTryReload { get; set; }
        public Action OnReloadStarted { get; set; }
        public Action OnReloadEnded { get; set; }

        public void TryStartReload()
        {
            OnTryReload?.Invoke();
        }

        public void StartReload()
        {
            IsRealoding = true;
            OnReloadStarted?.Invoke();
        }

        public void EndReload()
        {
            IsRealoding = false;
            OnReloadEnded?.Invoke();
        }
    }
}
