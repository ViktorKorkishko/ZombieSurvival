using System;
using Core.Installers;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.Weapons.Common.Views;
using UnityEngine;
using Zenject;

using Object = UnityEngine.Object;

namespace Game.Character.Weapons.PickUp.Controllers
{
    public class WeaponPickUpController : IInitializable, IDisposable, ITickable
    {
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }

        void IInitializable.Initialize()
        {

        }

        void IDisposable.Dispose()
        {

        }

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool pickUpWeapon = InputModel.PickUpWeaponButtonClickInput;
            if (pickUpWeapon)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            // temp (for test)
            var weaponView = Object.FindObjectOfType<WeaponView>();
            var pickedWeapon = weaponView.PickUp();
            
            var weaponContext = weaponView.gameObject.GetComponentInChildren<GameObjectContext>();
            var equipData = GetEquipData(weaponContext.Container);

            WeaponEquipModel.Equip(equipData);
        }

        private EquipData GetEquipData(DiContainer container)
        {
            var weaponRoot = container.ResolveId<Transform>(BindingIdentifiers.Root);
            var leftHandGripTransform = container.ResolveId<Transform>(BindingIdentifiers.LeftHandGripTransform);
            var rightHandGripTransform = container.ResolveId<Transform>(BindingIdentifiers.RightHandGripTransform);
            
            var equipData = new EquipData(weaponRoot, leftHandGripTransform, rightHandGripTransform);
            
            return equipData;
        }
    }
}
