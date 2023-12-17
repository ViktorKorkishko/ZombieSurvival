using System;
using System.Collections;
using Core.Coroutines.Models;
using Game.Weapons.Shooting.Config;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Models
{
    public class WeaponReloadModel
    {
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }
        [Inject] private WeaponShootingConfig WeaponConfig { get; }

        public bool IsRealoding { get; private set; }

        public Action OnReloadingStarted { get; set; }
        public Action OnReloadingEnded { get; set; }

        private int _reloadCoroutineIndex = -1;

        public void TryReload()
        {
            bool canStartReloading = !IsRealoding;
            if (!canStartReloading)
                return;

            StartReloading();
        }

        private void EndReload()
        {
            IsRealoding = false;
            OnReloadingEnded?.Invoke();
            Debug.Log("Reload ended");
        }

        private void StartReloading()
        {
            Debug.Log("Reload started");
            _reloadCoroutineIndex = CoroutinePlayerModel.StartCorotine(ReloadCo());
            IsRealoding = true;
            OnReloadingStarted?.Invoke();

            IEnumerator ReloadCo()
            {
                yield return new WaitForSeconds(WeaponConfig.ReloadTime);
                EndReload();
            }
        }
    }
}
