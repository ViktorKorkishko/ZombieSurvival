using System;
using System.Collections;
using Core.Coroutines.Models;
using Game.Weapons.Common.Config;
using Game.Weapons.Reload.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Controllers
{
    public class WeaponReloadController : IInitializable, IDisposable
    {
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }
        [Inject] private WeaponConfig WeaponConfig { get; }

        private bool IsReloading { get; set; }

        private int _reloadCoroutineIndex = -1;
        
        void IInitializable.Initialize()
        {
            WeaponReloadModel.OnTryReload += HandleOnTryReload;
            WeaponReloadModel.OnReloadStarted += HandleOnReloadingStarted;
        }

        void IDisposable.Dispose()
        {
            WeaponReloadModel.OnTryReload -= HandleOnTryReload;
            WeaponReloadModel.OnReloadStarted -= HandleOnReloadingStarted;
        }

        private void HandleOnTryReload()
        {
            bool canStartReloading = !IsReloading;
            if (!canStartReloading)
                return;
            
            WeaponReloadModel.StartReload();   
        }

        private void HandleOnReloadingStarted()
        {
            StartReloading();
        }

        private void StartReloading()
        {
            Debug.Log("Reload started");
            _reloadCoroutineIndex = CoroutinePlayerModel.StartCorotine(ReloadCo());

            IEnumerator ReloadCo()
            {
                yield return new WaitForSeconds(WeaponConfig.ReloadTime);
                EndReload();
            }
        }

        private void EndReload()
        {
            WeaponReloadModel.EndReload();
        }
    }
}
