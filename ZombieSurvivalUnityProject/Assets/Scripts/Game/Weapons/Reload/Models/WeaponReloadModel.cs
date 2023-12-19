using System;

namespace Game.Weapons.Reload.Models
{
    public class WeaponReloadModel
    {
        public bool IsReloading => OnCheckIsReloading?.Invoke() ?? false;

        public Func<bool> OnCheckIsReloading { get; set; }
        public Func<bool> OnTryReload { get; set; }
        public Action<Action> OnReloadStarted { get; set; }
        public Action OnReloadEnded { get; set; }

        public void TryStartReload()
        {
            bool startReload = OnTryReload?.Invoke() ?? false;

            if (startReload)
            {
                StartReload();
            }
        }

        private void StartReload()
        {
            OnReloadStarted?.Invoke(EndReload);
        }

        private void EndReload()
        {
            OnReloadEnded?.Invoke();
        }
    }
}
