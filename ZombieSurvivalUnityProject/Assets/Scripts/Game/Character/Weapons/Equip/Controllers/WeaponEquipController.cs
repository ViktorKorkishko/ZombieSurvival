using System;
using Core.Installers;
using Game.Character.Weapons.CurrentWeapon.Models;
using Game.Character.Weapons.Equip.Models;
using Game.Character.Weapons.PickUp.Models;
using Game.Inputs.Models;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable, ITickable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject] private WeaponPickUpModel WeaponPickUpModel { get; }
        [Inject] private CurrentWeaponModel CurrentWeaponModel { get; }
        [Inject] private InputModel InputModel { get; }

        [Inject] private RigBuilder RigBuilder { get; }

        [Inject(Id = BindingIdentifiers.CharacterAimRig)] private Rig AimRig { get; }

        [Inject(Id = BindingIdentifiers.CharacterHandsRig)] private Rig HandsRig { get; }

        [Inject(Id = BindingIdentifiers.WeaponHolder)] private Transform WeaponHolder { get; }

        [Inject(Id = BindingIdentifiers.IdleMultiParentConstraint)] private MultiParentConstraint IdleMultiParentConstraint { get; }

        [Inject(Id = BindingIdentifiers.AimMultiParentConstraint)] private MultiParentConstraint AimMultiParentConstraint { get; }

        [Inject(Id = BindingIdentifiers.LeftHandIKConstraint)] private TwoBoneIKConstraint LeftHandIKConstraint { get; }

        [Inject(Id = BindingIdentifiers.RightHandIKConstraint)] private TwoBoneIKConstraint RightHandIKConstraint { get; }

        private EquipData CurrentEquipData { get; set; }

        void IInitializable.Initialize()
        {
            WeaponEquipModel.OnWeaponEquipped += HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped += HandleOnWeaponUnequipped;
            WeaponPickUpModel.OnWeaponPickedUp += HandleOnWeaponPickedUp;
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
            SetRigAsWeaponEquipped(equipData);
            
            // CurrentWeaponModel.OnWeaponSet += HandleOnWeaponSet;
            //
            // void HandleOnWeaponSet(DiContainer weaponContainer)
            // {
            //     CurrentWeaponModel.OnWeaponSet -= HandleOnWeaponSet;
            //     
            //     var weaponEquipModel = weaponContainer.Resolve<Game.Weapons.Equip.Models.WeaponEquipModel>();
            //     weaponEquipModel.Equip();
            // }
        }

        private void SetRigAsWeaponEquipped(EquipData equipData)
        {
            // set to "weapon slot"
            var weaponRoot = equipData.WeaponWeaponRoot;
            weaponRoot.SetParent(WeaponHolder);
            weaponRoot.localPosition = Vector3.zero;

            // update data
            IdleMultiParentConstraint.data.constrainedObject = weaponRoot;
            AimMultiParentConstraint.data.constrainedObject = weaponRoot;
            LeftHandIKConstraint.data.target = equipData.LeftHandGripTransform;
            RightHandIKConstraint.data.target = equipData.RightHandGripTransform;

            // rebuild rig
            RigBuilder.Build();

            AimRig.weight = 1.0f;
            HandsRig.weight = 1.0f;
        }

        private void HandleOnWeaponUnequipped()
        {
            SetRigAsWeaponUnequipped();
            var weaponEquipModel = CurrentWeaponModel.WeaponContainer.Resolve<Game.Weapons.Equip.Models.WeaponEquipModel>();
            weaponEquipModel.Unequip();
        }

        private void SetRigAsWeaponUnequipped()
        {
            var weaponRoot = CurrentEquipData.WeaponWeaponRoot;
            weaponRoot.SetParent(null);

            IdleMultiParentConstraint.data.constrainedObject = null;
            AimMultiParentConstraint.data.constrainedObject = null;
            LeftHandIKConstraint.data.target = null;
            RightHandIKConstraint.data.target = null;

            CurrentEquipData = null;

            RigBuilder.Build();

            AimRig.weight = 0f;
            HandsRig.weight = 0f;
        }

        private void HandleOnWeaponPickedUp(DiContainer weaponContainer)
        {
            var equipData = GetEquipData(weaponContainer);

            CurrentWeaponModel.SetCurrentWeapon(weaponContainer);
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
