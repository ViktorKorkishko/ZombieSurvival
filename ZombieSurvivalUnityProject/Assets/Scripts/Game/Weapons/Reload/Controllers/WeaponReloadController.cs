using System;
using System.Collections;
using Core.Coroutines.Controllers;
using Game.Weapons.Common.Config;
using Game.Weapons.Reload.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Reload.Controllers
{
    public class WeaponReloadController : IInitializable, IDisposable
    {
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private CoroutinePlayerPresenter CoroutinePlayerPresenter { get; }
        [Inject] private WeaponConfig WeaponConfig { get; }

        private bool IsReloading { get; set; }

        private int _reloadCoroutineIndex = -1;

        private Action OnReloadEnded { get; set; }

        void IInitializable.Initialize()
        {
            WeaponReloadModel.OnCheckIsReloading += HandleOnCheckIsReloading;
            WeaponReloadModel.OnTryReload += HandleOnTryReload;
            WeaponReloadModel.OnTryTerminateReload += HandleOnTryTerminateReload;
            WeaponReloadModel.OnReloadStarted += HandleOnReloadingStarted;
        }

        void IDisposable.Dispose()
        {
            WeaponReloadModel.OnCheckIsReloading -= HandleOnCheckIsReloading;
            WeaponReloadModel.OnTryReload -= HandleOnTryReload;
            WeaponReloadModel.OnTryTerminateReload -= HandleOnTryTerminateReload;
            WeaponReloadModel.OnReloadStarted -= HandleOnReloadingStarted;

            CoroutinePlayerPresenter.StopCoroutine(_reloadCoroutineIndex);
        }

        private bool HandleOnCheckIsReloading()
        {
            return IsReloading;
        }

        private bool HandleOnTryReload()
        {
            bool canStartReloading = !IsReloading;
            return canStartReloading;
        }

        private void HandleOnTryTerminateReload()
        {
            TryTerminateReload();
        }

        private void HandleOnReloadingStarted(Action onReloadEnded)
        {
            OnReloadEnded = onReloadEnded;

            StartReloading();
        }

        private void StartReloading()
        {
            Debug.Log("Reload started");
            IsReloading = true;
            
            _reloadCoroutineIndex = CoroutinePlayerPresenter.HandleOnCoroutineStarted(ReloadCo());

            IEnumerator ReloadCo()
            {
                yield return new WaitForSeconds(WeaponConfig.ReloadTime);
                EndReload();
            }
        }

        private void EndReload()
        {
            IsReloading = false;
            OnReloadEnded?.Invoke();
            OnReloadEnded = null;
        }

        private void TryTerminateReload()
        {
            var terminateReload = IsReloading;
            if (terminateReload)
            {
                TerminateReload();
            }

            void TerminateReload()
            {
                CoroutinePlayerPresenter.StopCoroutine(_reloadCoroutineIndex);
                IsReloading = false;
                
                Debug.Log("Reload terminated");
            }
        }
    }
}