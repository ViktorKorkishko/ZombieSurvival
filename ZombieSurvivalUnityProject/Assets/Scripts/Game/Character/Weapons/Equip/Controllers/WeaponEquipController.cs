﻿using System;
using Core.Installers;
using Game.Animations;
using Game.Character.Movement.Locomotion.Models;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.Weapons.Common;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable, ITickable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private CharacterLocomotionModel CharacterLocomotionModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponsAnimatorStatesNamesProvider WeaponsAnimatorStatesNamesProvider { get; }
        [Inject(Id = BindingIdentifiers.CharacterRigAnimator)] private Animator CharacterRigAnimator { get; set; }
        [Inject(Id = BindingIdentifiers.WeaponHolder)] private Transform WeaponHolder { get; }
        [Inject(Id = BindingIdentifiers.SprintParamId)] private string SprintParamId { get; }
        [Inject(Id = BindingIdentifiers.UnarmedStateName)] private string UnarmedStateName { get; }
        
        private EquipData CurrentEquipData { get; set; }

        void IInitializable.Initialize()
        {
            WeaponEquipModel.OnWeaponEquipped += HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped += HandleOnWeaponUnequipped;
            WeaponPickUpModel.OnWeaponPickedUp += HandleOnWeaponPickedUp;
            CharacterLocomotionModel.OnStartedRunning += HandleOnStartedRunning;
            CharacterLocomotionModel.OnEndedRunning += HandleOnEndedRunning;

            SetRigAsWeaponUnequipped();
        }

        void IDisposable.Dispose()
        {
            WeaponEquipModel.OnWeaponEquipped -= HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped -= HandleOnWeaponUnequipped;
            WeaponPickUpModel.OnWeaponPickedUp -= HandleOnWeaponPickedUp;
            CharacterLocomotionModel.OnStartedRunning -= HandleOnStartedRunning;
            CharacterLocomotionModel.OnEndedRunning -= HandleOnEndedRunning;
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
            var weaponId = CurrentWeaponModel.WeaponContainer.Resolve<WeaponId>();
            if (WeaponsAnimatorStatesNamesProvider.TryGetWeaponAnimationsContainer(weaponId, out var weaponAnimationsContainer))
            {
                CharacterRigAnimator.Play(weaponAnimationsContainer.EquipAnimatorStateName);
            }
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

        private void SetRigAsWeaponUnequipped()
        {
            CharacterRigAnimator.Play(UnarmedStateName);
        }
        
        private void HandleOnStartedRunning()
        {
            CharacterRigAnimator.SetBool(SprintParamId, true);
        }

        private void HandleOnEndedRunning()
        {
            CharacterRigAnimator.SetBool(SprintParamId, false);
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
