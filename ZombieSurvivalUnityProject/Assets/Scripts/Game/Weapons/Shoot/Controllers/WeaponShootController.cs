using System;
using Game.Cameras.Models;
using Game.Weapons.Common.Config;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shoot.Models;
using Game.Weapons.Shoot.Views;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shoot.Controllers
{
    public class WeaponShootController : IInitializable, IDisposable
    {
        [Inject] private WeaponShootModel WeaponShootModel { get; }
        [Inject] private WeaponMagazineModel WeaponMagazineModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private WeaponShootView WeaponShootView { get; }
        [Inject] private CameraModel CameraModel { get; }
        [Inject] private WeaponConfig WeaponConfig { get; }
        [Inject] private LayerMask LayerMask { get; }

        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;

        void IInitializable.Initialize()
        {
            WeaponShootModel.OnTryShoot += HandleOnTryShoot;
            WeaponShootModel.OnShoot += HandleOnShoot;
        }

        void IDisposable.Dispose()
        {
            WeaponShootModel.OnTryShoot -= HandleOnTryShoot;
            WeaponShootModel.OnShoot -= HandleOnShoot;
        }

        private bool HandleOnTryShoot()
        {
            bool haveBullets = WeaponMagazineModel.BulletsLeft > 0;
            bool canShoot = haveBullets && !WeaponReloadModel.IsReloading;
            
            return canShoot;
        }

        private void HandleOnShoot() => Shoot();

        private void Shoot()
        {
            WeaponShootView.EmitFireFlash();
            
            var rayStartPoint = MainCameraTransform.position;
            var rayDirection = MainCameraTransform.forward;
            
            if (Physics.Raycast(rayStartPoint, rayDirection, out RaycastHit raycastHit, WeaponConfig.Range, LayerMask))
            {
                Debug.Log(raycastHit.collider.name);
            }
        }
    }
}
