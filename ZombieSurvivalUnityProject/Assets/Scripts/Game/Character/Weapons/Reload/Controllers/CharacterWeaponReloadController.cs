using System;
using Core.Installers;
using Game.Animations;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.Reload.Models;
using Game.Character.Weapons.Reload.Views;
using Game.Inputs.Models;
using Game.Weapons.Common;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Character.Weapons.Reload.Controllers
{
    public class CharacterWeaponReloadController : IInitializable, IDisposable, ITickable
    {
        [Inject] private InputModel InputModel { get; }
        [Inject] private CharacterWeaponReloadModel CharacterWeaponReloadModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }

        [Inject(Id = BindingIdentifiers.CharacterRigAnimator)] private Animator CharacterRigAnimator { get; }
        [Inject] private WeaponsAnimatorStatesNamesProvider WeaponsAnimatorStatesNamesProvider { get; }
        [Inject] private CharacterWeaponReloadAnimationView CharacterWeaponReloadAnimationView { get; }
        [Inject(Id = BindingIdentifiers.CharacterLeftHandTransform)] private Transform CharacterLeftHandTransform { get; }
        
        private Game.Weapons.Reload.Models.WeaponReloadModel Weapon { get; set; }

        void IInitializable.Initialize()
        {
            CurrentWeaponModel.OnWeaponSet += HandleOnCurrentWeaponSet;
            CharacterWeaponReloadModel.OnTryReload += HandleOnTryReload;
            CharacterWeaponReloadAnimationView.OnTriggerReloadEvent += HandleOnTriggerReloadEvent;
        }

        void IDisposable.Dispose()
        {
            CurrentWeaponModel.OnWeaponSet -= HandleOnCurrentWeaponSet;
            CharacterWeaponReloadModel.OnTryReload -= HandleOnTryReload;
            CharacterWeaponReloadAnimationView.OnTriggerReloadEvent -= HandleOnTriggerReloadEvent;
        }

        void ITickable.Tick()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            bool reloadButtonClicked = InputModel.ReloadButtonClickInput;
            if (reloadButtonClicked)
            {
                CharacterWeaponReloadModel.TryReload();
            }
        }

        private void HandleOnCurrentWeaponSet(EquippedWeapon equippedWeapon)
        {
            var isWeaponEquipped = Weapon != null;
            if (isWeaponEquipped)
            {
                Weapon.TryTerminateReload();
            }
            
            var newWeaponEquipped = equippedWeapon != null;
            if (newWeaponEquipped)
            {
                Weapon = equippedWeapon.GetComponent<Game.Weapons.Reload.Models.WeaponReloadModel>();
            }
        }

        private void HandleOnTryReload()
        {
            bool weaponEquipped = CurrentWeaponModel.IsWeaponEquipped;
            if (!weaponEquipped)
                return;

            var weaponId = CurrentWeaponModel.EquippedWeapon.GetComponent<WeaponId>();
            if (WeaponsAnimatorStatesNamesProvider.TryGetWeaponAnimationsContainer(weaponId, out var weaponAnimationsContainer))
            {
                var reloadTriggerName = weaponAnimationsContainer.ReloadAnimationTriggerName;
                CharacterRigAnimator.SetTrigger(reloadTriggerName);
                Weapon.TryStartReload();
            }
        }
        
        // TODO: use object pooling and wrap into separate module
        // temp implementation
        GameObject originalMagazine => CurrentWeaponModel.EquippedWeapon.GetComponentWithId<GameObject>(BindingIdentifiers.MagazineGameObject);
        private GameObject detachedMagazine;
        private GameObject newMagazine;
        private void HandleOnTriggerReloadEvent(ReloadAnimationEventId reloadAnimationEventId)
        {
            switch (reloadAnimationEventId)
            {
                case ReloadAnimationEventId.DetachMagazine:
                    originalMagazine.SetActive(false);
                    detachedMagazine = Object.Instantiate(originalMagazine, CharacterLeftHandTransform, true);
                    detachedMagazine.SetActive(true);
                    break;
                
                case ReloadAnimationEventId.DropMagazine:
                    detachedMagazine.AddComponent<BoxCollider>();
                    detachedMagazine.AddComponent<Rigidbody>();
                    detachedMagazine.transform.SetParent(null);
                    break;
                
                case ReloadAnimationEventId.RefillMagazine:
                    var handTransform = CharacterLeftHandTransform;
                    newMagazine = Object.Instantiate(originalMagazine, handTransform.position, handTransform.rotation, handTransform);
                    newMagazine.SetActive(true);
                    break;
                
                case ReloadAnimationEventId.AttachMagazine:
                    Object.Destroy(newMagazine);
                    originalMagazine.SetActive(true);
                    break;
                
                default:
                    // TODO: throw error
                    break;
            }
        }
        //
    }
}
