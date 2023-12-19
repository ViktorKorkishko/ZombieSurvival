using Core.Installers;
using Game.Cameras.Models;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.Weapons.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.PickUp.Controllers
{
    public class WeaponPickUpController : ITickable
    {
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private CameraModel CameraModel { get; }

        private Transform MainCameraTransform => CameraModel.GetMainCamera().transform;

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool pickUpWeaponInput = InputModel.PickUpWeaponButtonClickInput;
            if (!pickUpWeaponInput)
                return;
            
            var weapon = CheckForObjectsToInteract();
            bool haveWeaponToPickUp = weapon != null;
            if (!haveWeaponToPickUp)
            {
                Debug.Log("noting");
                return;
            }
            
            Debug.Log(weapon.gameObject);
            PickUp(weapon);
        }

        private WeaponView CheckForObjectsToInteract()
        {
            Vector3 origin = MainCameraTransform.position;
            Vector3 direction = MainCameraTransform.forward;
            float rayDistance = WeaponPickUpModel.MaxPickUpDistance;

            if (Physics.Raycast(origin, direction, out var raycastHit, rayDistance))
            {
                var weapon = raycastHit.collider.GetComponentInParent<WeaponView>();
                return weapon;
            }

            return null;
        }

        private void PickUp(WeaponView weaponView)
        {
            var weaponContainer = weaponView.gameObject.GetComponentInChildren<GameObjectContext>().Container;
            WeaponPickUpModel.PickUp(weaponContainer);
        }
    }
}
