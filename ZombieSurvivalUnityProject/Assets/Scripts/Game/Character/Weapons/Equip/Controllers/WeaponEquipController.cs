using System;
using Core.Installers.Ids;
using Game.Character.Weapons.Equip.Models;
using Game.Weapons.Common.Models;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        
        [Inject(Id = BindingIdentifiers.CharacterAimRig)] private RigBuilder RigBuilder { get; }
        [Inject(Id = BindingIdentifiers.CharacterAimRig)] private Rig AimRig { get; }
        [Inject(Id = BindingIdentifiers.WeaponHolder)] private Transform WeaponHolder { get; }
        [Inject(Id = BindingIdentifiers.LeftHandIKConstraint)] private TwoBoneIKConstraint LeftHandIKConstraint { get; }
        [Inject(Id = BindingIdentifiers.RightHandIKConstraint)] private TwoBoneIKConstraint RightHandIKConstraint { get; }

        void IInitializable.Initialize()
        {
            WeaponEquipModel.OnWeaponEquipped += HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped += HandleOnWeaponEquipped;
        }

        void IDisposable.Dispose()
        {
            WeaponEquipModel.OnWeaponEquipped -= HandleOnWeaponEquipped;
            WeaponEquipModel.OnWeaponUnequipped -= HandleOnWeaponEquipped;
        }

        private void HandleOnWeaponEquipped(EquipData equipData)
        {
            EquipWeapon(equipData);
        }

        private void EquipWeapon(EquipData equipData)
        {
            // update data
            LeftHandIKConstraint.data.target = equipData.LeftHandGripTransform;
            RightHandIKConstraint.data.target = equipData.RightHandGripTransform;
            
            // rebuild rig
            // RigBuilder.Build();
            
            AimRig.weight = 1.0f;
        }

        private void SetRigAsWeaponUnequipped()
        {
            AimRig.weight = 0f;
        }
    }
}
