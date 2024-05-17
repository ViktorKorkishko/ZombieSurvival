using System;
using Core.Installers;
using Core.Lifetime.Instantiation;
using Game.Animations;
using Game.Character.Movement.Locomotion.Models;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using Game.ItemsDB;
using Game.Weapons.Facade;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

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

        private WeaponFacade CurrentlyEquippedWeapon { get; set; }

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

        private void EquipWeapon(WeaponFacade weapon)
        {
            CurrentlyEquippedWeapon = weapon;
            
            AttachWeapon();
            SetRigAsWeaponEquipped();

            void AttachWeapon()
            {
                var weaponId = CurrentlyEquippedWeapon.WeaponId;
                Debug.Log(weaponId);
                
                var weaponRoot = CurrentlyEquippedWeapon.Root;
                weaponRoot.SetParent(WeaponHolder);
                weaponRoot.localPosition = Vector3.zero;
                weaponRoot.localRotation = Quaternion.identity;
            }

            void SetRigAsWeaponEquipped()
            {
                var weaponId = CurrentlyEquippedWeapon.WeaponId;
                if (WeaponsAnimatorStatesNamesProvider.TryGetWeaponAnimationsContainer(weaponId,
                        out var weaponAnimationsContainer))
                {
                    CharacterRigAnimator.Play(weaponAnimationsContainer.EquipAnimatorStateName);
                }
            }
        }
        
        private void UnequipWeapon()
        {
            DestroyWeaponGameObject();
            SetRigAsWeaponUnequipped();
            
            void DestroyWeaponGameObject()
            {
                // TODO: return to pool (use pool in future)
                Object.Destroy(CurrentlyEquippedWeapon.gameObject);
                
                CurrentWeaponModel.SetCurrentWeapon(null);
                CurrentlyEquippedWeapon = null;
            }
        }
        
        private void HandleOnWeaponEquipped(WeaponFacade weapon)
        {
            if (CurrentWeaponModel.IsWeaponEquipped)
            {
                CharacterWeaponEquipModel.Unequip();
            }

            EquipWeapon(weapon);
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

        private void HandleOnWeaponPickedUp(WeaponFacade weapon)
        {
            CharacterWeaponEquipModel.Equip(weapon);
            CurrentWeaponModel.SetCurrentWeapon(weapon);
        }

        #endregion
    }
}
