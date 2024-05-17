using System;
using Core.Installers;
using Game.Weapons.Equip.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject] private Collider Collider { get; }
        [Inject] private Rigidbody Rigidbody { get; }
        [Inject(Id = BindingIdentifiers.ViewRoot)] private Transform ViewRoot { get; }

        void IInitializable.Initialize()
        {
            WeaponEquipModel.OnEquipped += HandleOnEquipped;
            WeaponEquipModel.OnUnequipped += HandleOnUnequipped;
        }

        void IDisposable.Dispose()
        {
            WeaponEquipModel.OnEquipped -= HandleOnEquipped;
            WeaponEquipModel.OnUnequipped -= HandleOnUnequipped;
        }

        private void HandleOnEquipped()
        {
            DisableWeaponPhysics();
        }

        private void HandleOnUnequipped()
        {
            EnableWeaponPhysics();
        }

        private void EnableWeaponPhysics()
        {
            Collider.enabled = true;
            Rigidbody.useGravity = true;
            Rigidbody.constraints = RigidbodyConstraints.None;
        }

        private void DisableWeaponPhysics()
        {
            Collider.enabled = false;
            Rigidbody.useGravity = false;
            ViewRoot.localPosition = Vector3.zero;
            ViewRoot.localRotation = Quaternion.identity;
            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
