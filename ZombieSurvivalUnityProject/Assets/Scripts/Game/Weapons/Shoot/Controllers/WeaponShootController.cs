using System;
using Core.Installers;
using Game.Cameras.Models;
using Game.Weapons.Common.Config;
using Game.Weapons.Reload.Models;
using Game.Weapons.Shoot.Models;
using Game.Weapons.Shoot.Views;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

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
        [Inject(Id = BindingIdentifiers.BulletTrailRenderer)] private TrailRenderer BulletTrailRenderer { get; }
        [Inject(Id = BindingIdentifiers.WeaponShootStartPointTransform)] private Transform WeaponShootStartPointTransform { get; }
        [Inject(Id = BindingIdentifiers.CrosshairTargetPointTransform)] private Transform CrosshairTargetPointTransform { get; }
        
        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;
        
        // 60 sec / fireRate (shots per minute)
        private float TimeBetweenShots => 60f / WeaponConfig.FireRate;
        private float _lastShotTime = 0f;

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
            bool readyToFireAccordingToFireRate = Time.time - _lastShotTime >= TimeBetweenShots;
            bool haveBullets = WeaponMagazineModel.BulletsLeft > 0;
            bool reloading = WeaponReloadModel.IsReloading;
            
            bool canShoot = readyToFireAccordingToFireRate && haveBullets && !reloading;
            return canShoot;
        }

        private void HandleOnShoot() => Shoot();

        private void Shoot()
        {
            _lastShotTime = Time.time;
                
            WeaponShootView.EmitFireFlash();
            
            var rayStartPoint = WeaponShootStartPointTransform.position;
            var rayDirection = (CrosshairTargetPointTransform.position - rayStartPoint).normalized;

            var tracer = Object.Instantiate(BulletTrailRenderer, rayStartPoint, Quaternion.identity);
            tracer.AddPosition(rayStartPoint);
            
            if (Physics.Raycast(rayStartPoint, rayDirection, out RaycastHit raycastHit, WeaponConfig.Range, LayerMask))
            {
                var point = raycastHit.point;
                var normal = raycastHit.normal;
                WeaponShootView.EmitShotEffect(point, normal);
                
                tracer.transform.position = point;
            }
        }
    }
}
