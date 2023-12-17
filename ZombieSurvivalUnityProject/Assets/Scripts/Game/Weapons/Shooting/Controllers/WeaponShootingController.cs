using System;
using Game.Cameras.Models;
using Game.Inputs.Models;
using Game.Weapons.Shooting.Config;
using Game.Weapons.Shooting.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Shooting.Controllers
{
    public class WeaponShootingController : IInitializable, ITickable, IDisposable
    {
        [Inject] private WeaponShootingModel WeaponShootingModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private CameraModel CameraModel { get; }
        [Inject] private WeaponShootingConfig WeaponConfig { get; }
        [Inject] private LayerMask LayerMask { get; }

        private Transform mainCameraTransfom => CameraModel.GetMainCamera().transform;

        public void Initialize()
        {
            WeaponShootingModel.OnShoot += HandleOnShoot;
        }

        public void Dispose()
        {
            WeaponShootingModel.OnShoot -= HandleOnShoot;
        }

        public void Tick()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            bool fireButtonClicked = InputModel.LeftMouseButtonClicked;
            if (fireButtonClicked)
            {
                WeaponShootingModel.TryShoot();
            }
        }

        private void HandleOnShoot() => Shoot();

        private void Shoot()
        {
            var rayStartPoint = mainCameraTransfom.position;
            var rayDirection = mainCameraTransfom.forward;

            // TODO: inject layer
            if (Physics.Raycast(rayStartPoint, rayDirection, out RaycastHit raycastHit, WeaponConfig.Range, LayerMask))
            {
                Debug.Log(raycastHit.collider.name);
                raycastHit.collider.GetComponent<ShootingAi>().TakeDamage(WeaponConfig.Damage);
            }
        }
    }
}
