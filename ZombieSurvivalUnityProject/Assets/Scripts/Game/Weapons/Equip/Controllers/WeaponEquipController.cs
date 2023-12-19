using System;
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
            Collider.enabled = false;
            Rigidbody.useGravity = false;
        }

        private void HandleOnUnequipped()
        {
            Collider.enabled = true;
            Rigidbody.useGravity = true;
        }
    }
}