using System;
using Core.Installers;
using Game.Animations;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Reload.Models;
using Game.Character.Weapons.Reload.Views;
using Game.Inputs.Models;
using Game.Weapons.Common;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Character.Weapons.Reload.Controllers
{
    public class WeaponReloadController : IInitializable, IDisposable, ITickable
    {
        [Inject] private InputModel InputModel { get; }
        [Inject] private WeaponReloadModel WeaponReloadModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject(Id = BindingIdentifiers.CharacterRigAnimator)] private Animator CharacterRigAnimator { get; }
        [Inject] private WeaponsAnimatorStatesNamesProvider WeaponsAnimatorStatesNamesProvider { get; }
        [Inject] private WeaponReloadAnimationView WeaponReloadAnimationView { get; }
        [Inject(Id = BindingIdentifiers.CharacterLeftHandTransform)] private Transform CharacterLeftHandTransform { get; }
        
        private Game.Weapons.Reload.Models.WeaponReloadModel Weapon { get; set; }

        void IInitializable.Initialize()
        {
            CurrentWeaponModel.OnWeaponSet += HandleOnCurrentWeaponSet;
            WeaponReloadModel.OnTryReload += HandleOnTryReload;
            
            WeaponReloadAnimationView.OnTriggerReloadEvent += HandleOnTriggerReloadEvent;
        }

        void IDisposable.Dispose()
        {
            CurrentWeaponModel.OnWeaponSet -= HandleOnCurrentWeaponSet;
            WeaponReloadModel.OnTryReload -= HandleOnTryReload;
            
            WeaponReloadAnimationView.OnTriggerReloadEvent -= HandleOnTriggerReloadEvent;
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
                WeaponReloadModel.TryReload();
            }
        }

        private void HandleOnCurrentWeaponSet(DiContainer weaponContainer)
        {
            Weapon = CurrentWeaponModel.IsWeaponEquipped ? 
                weaponContainer.Resolve<Game.Weapons.Reload.Models.WeaponReloadModel>() : 
                null;
        }

        private void HandleOnTryReload()
        {
            bool weaponEquipped = CurrentWeaponModel.IsWeaponEquipped;
            if (!weaponEquipped)
                return;

            var weaponId = CurrentWeaponModel.WeaponContainer.Resolve<WeaponId>();
            if (WeaponsAnimatorStatesNamesProvider.TryGetWeaponAnimationsContainer(weaponId, out var weaponAnimationsContainer))
            {
                var reloadTriggerName = weaponAnimationsContainer.ReloadAnimationTriggerName;
                CharacterRigAnimator.SetTrigger(reloadTriggerName);
                Weapon.TryStartReload();
            }
        }

        GameObject originalMagazine => CurrentWeaponModel.WeaponContainer.ResolveId<GameObject>(BindingIdentifiers.MagazineGameObject);
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
                    var viewRoot = CurrentWeaponModel.WeaponContainer.ResolveId<Transform>(BindingIdentifiers.ViewRoot);
                    var handTransform = CharacterLeftHandTransform;
                    newMagazine = Object.Instantiate(originalMagazine, handTransform.position, handTransform.rotation, handTransform);
                    newMagazine.SetActive(true);
                    break;
                
                case ReloadAnimationEventId.AttachMagazine:
                    Object.Destroy(newMagazine);
                    originalMagazine.SetActive(true);
                    break;
                
                default:
                    // throw error
                    break;
            }
        }
    }
}
