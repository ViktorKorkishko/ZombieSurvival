using System;
using Core.Installers;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable, ITickable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject(Id = BindingIdentifiers.CharacterRigAnimator)] private Animator CharacterRigAnimator { get; set; }
        [Inject(Id = BindingIdentifiers.WeaponHolder)] private Transform WeaponHolder { get; }
        
        private EquipData CurrentEquipData { get; set; }

        void IInitializable.Initialize()
        {
            WeaponEquipModel.OnWeaponEquipped += HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped += HandleOnWeaponUnequipped;
            WeaponPickUpModel.OnWeaponPickedUp += HandleOnWeaponPickedUp;

            SetRigAsWeaponUnequipped();
        }

        void IDisposable.Dispose()
        {
            WeaponEquipModel.OnWeaponEquipped -= HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped -= HandleOnWeaponUnequipped;
            WeaponPickUpModel.OnWeaponPickedUp -= HandleOnWeaponPickedUp;
        }

        void ITickable.Tick()
        {
            bool dropButtonClickInput = InputModel.DropWeaponButtonClickInput;
            if (!dropButtonClickInput)
                return;

            bool weaponEquipped = CurrentWeaponModel.IsWeaponEquipped;
            if (!weaponEquipped)
                return;
            
            WeaponEquipModel.Unequip();
            CurrentWeaponModel.SetCurrentWeapon(null);
        }

        private void HandleOnWeaponEquipped(EquipData equipData)
        {
            var weaponEquipModel = CurrentWeaponModel.WeaponContainer.Resolve<Game.Weapons.Equip.Models.WeaponEquipModel>();
            weaponEquipModel.Equip();
            
            CurrentEquipData = equipData;
            
            AttachWeapon();
            SetRigAsWeaponEquipped();
            
            void AttachWeapon()
            {
                var weaponRoot = equipData.WeaponRoot;
                weaponRoot.SetParent(WeaponHolder);
                weaponRoot.localPosition = Vector3.zero;
                weaponRoot.localRotation = Quaternion.identity;
            }
        }

        private void SetRigAsWeaponEquipped()
        {
            var animation = CurrentWeaponModel.WeaponContainer.Resolve<string>();
            CharacterRigAnimator.Play(animation);
        }

        private void HandleOnWeaponUnequipped()
        {
            TryDetachWeapon();
            SetRigAsWeaponUnequipped();
            
            CurrentEquipData = null;
            
            var weaponEquipModel = CurrentWeaponModel.WeaponContainer.Resolve<Game.Weapons.Equip.Models.WeaponEquipModel>();
            weaponEquipModel.Unequip();

            void TryDetachWeapon()
            {
                if (!CurrentWeaponModel.IsWeaponEquipped)
                    return;
            
                var weaponRoot = CurrentEquipData.WeaponRoot;
                weaponRoot.SetParent(null);
            }
        }

        [Inject(Id = BindingIdentifiers.UnarmedStateName)] private string UnarmedStateName { get; }

        private void SetRigAsWeaponUnequipped()
        {
            // HandsRig.weight = 0f;
            // Animator.SetLayerWeight(1, 0f);
            
            CharacterRigAnimator.Play(UnarmedStateName);
        }

        private void HandleOnWeaponPickedUp(DiContainer weaponContainer)
        {
            var equipData = GetEquipData(weaponContainer);

            CurrentWeaponModel.SetCurrentWeapon(weaponContainer);
            WeaponEquipModel.Equip(equipData);
        }

        private EquipData GetEquipData(DiContainer weaponContainer)
        {
            var weaponRoot = weaponContainer.ResolveId<Transform>(BindingIdentifiers.Root);
            
            var equipData = new EquipData(weaponRoot);

            return equipData;
        }
    }
}
