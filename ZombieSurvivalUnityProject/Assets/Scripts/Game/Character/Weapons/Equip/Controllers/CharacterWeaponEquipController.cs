using System;
using Core.Installers;
using Game.Animations;
using Game.Character.Movement.Locomotion.Models;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.Weapons.Common;
using Game.Weapons.Equip.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class CharacterWeaponEquipController : IInitializable, IDisposable, ITickable
    {
        [Inject] private CharacterWeaponEquipModel CharacterWeaponEquipModel { get; }
        [Inject] private CharacterWeaponPickUpModel CharacterWeaponPickUpModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private CharacterLocomotionModel CharacterLocomotionModel { get; }
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponsAnimatorStatesNamesProvider WeaponsAnimatorStatesNamesProvider { get; }
        [Inject(Id = BindingIdentifiers.CharacterRigAnimator)] private Animator CharacterRigAnimator { get; set; }
        [Inject(Id = BindingIdentifiers.WeaponHolder)] private Transform WeaponHolder { get; }
        [Inject(Id = BindingIdentifiers.SprintParamId)] private string SprintParamId { get; }
        [Inject(Id = BindingIdentifiers.UnarmedStateName)] private string UnarmedStateName { get; }

        private EquippedWeapon CurrentEquippedWeapon { get; set; }
        private WeaponEquipModel WeaponEquipModel { get; set; }

        void IInitializable.Initialize()
        {
            CharacterWeaponEquipModel.OnWeaponEquipped += HandleOnWeaponEquipped;
            CharacterWeaponEquipModel.OnWeaponUnequipped += HandleOnWeaponUnequipped;
            CharacterWeaponPickUpModel.OnWeaponPickedUp += HandleOnWeaponPickedUp;
            CharacterLocomotionModel.OnStartedRunning += HandleOnStartedRunning;
            CharacterLocomotionModel.OnEndedRunning += HandleOnEndedRunning;

            SetRigAsWeaponUnequipped();
        }

        void IDisposable.Dispose()
        {
            CharacterWeaponEquipModel.OnWeaponEquipped -= HandleOnWeaponEquipped;
            CharacterWeaponEquipModel.OnWeaponUnequipped -= HandleOnWeaponUnequipped;
            CharacterWeaponPickUpModel.OnWeaponPickedUp -= HandleOnWeaponPickedUp;
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

            CharacterWeaponEquipModel.Unequip();
        }

        #region Equip

        private void EquipWeapon(EquippedWeapon equippedWeapon)
        {
            Debug.Log("Start equipping");
            CurrentEquippedWeapon = equippedWeapon;
            
            Debug.Log("Start attaching");
            AttachWeapon();
            Debug.Log("End attaching");
            
            Debug.Log("Start equip rigging");
            SetRigAsWeaponEquipped();
            Debug.Log("End equip rigging");
            
            var weaponEquipModel = CurrentEquippedWeapon.GetComponent<Game.Weapons.Equip.Models.WeaponEquipModel>();
            weaponEquipModel.Equip();
            
            Debug.Log("End equipping");

            void AttachWeapon()
            {
                var weaponId = CurrentEquippedWeapon.GetComponent<WeaponId>();
                Debug.Log(weaponId);
                
                var weaponRoot = CurrentEquippedWeapon.GetComponentWithId<Transform>(BindingIdentifiers.Root);
                weaponRoot.SetParent(WeaponHolder);
                weaponRoot.localPosition = Vector3.zero;
                weaponRoot.localRotation = Quaternion.identity;
            }

            void SetRigAsWeaponEquipped()
            {
                var weaponId = CurrentEquippedWeapon.GetComponent<WeaponId>();
                if (WeaponsAnimatorStatesNamesProvider.TryGetWeaponAnimationsContainer(weaponId,
                        out var weaponAnimationsContainer))
                {
                    CharacterRigAnimator.Play(weaponAnimationsContainer.EquipAnimatorStateName);
                }
            }
        }

        private void UnequipWeapon()
        {
            Debug.Log("Start unequipping");
            
            Debug.Log("Start dettaching");
            TryDetachWeapon();
            Debug.Log("End dettaching");
            
            Debug.Log("Start unequip rigging");
            SetRigAsWeaponUnequipped();
            Debug.Log("End unequip rigging");

            var weaponEquipModel = CurrentEquippedWeapon.GetComponent<Game.Weapons.Equip.Models.WeaponEquipModel>();
            weaponEquipModel.Unequip();

            CurrentWeaponModel.SetCurrentWeapon(null);
            CurrentEquippedWeapon = null;
            
            Debug.Log("End unequipping");

            void TryDetachWeapon()
            {
                // Debug.Break();
                var weaponId = CurrentEquippedWeapon.GetComponent<WeaponId>();
                Debug.Log(weaponId);
                
                var weaponRoot = CurrentEquippedWeapon.GetComponentWithId<Transform>(BindingIdentifiers.Root);
                weaponRoot.SetParent(null);
                // Debug.Break();
            }
        }

        private void HandleOnWeaponEquipped(EquippedWeapon equippedWeapon)
        {
            if (CurrentWeaponModel.IsWeaponEquipped)
            {
                CharacterWeaponEquipModel.Unequip();
                // Debug.Break();
            }

            EquipWeapon(equippedWeapon);
            // Debug.Break();
        }

        private void HandleOnWeaponUnequipped()
        {
            UnequipWeapon();
        }

        private void SetRigAsWeaponUnequipped()
        {
            CharacterRigAnimator.Play(UnarmedStateName);
        }

        #endregion

        #region Running

        private void HandleOnStartedRunning()
        {
            CharacterRigAnimator.SetBool(SprintParamId, true);
        }

        private void HandleOnEndedRunning()
        {
            CharacterRigAnimator.SetBool(SprintParamId, false);
        }

        #endregion

        #region PickUp

        private void HandleOnWeaponPickedUp(EquippedWeapon equippedWeapon)
        {
            CharacterWeaponEquipModel.Equip(equippedWeapon);
            CurrentWeaponModel.SetCurrentWeapon(equippedWeapon);
        }

        #endregion
    }
}
