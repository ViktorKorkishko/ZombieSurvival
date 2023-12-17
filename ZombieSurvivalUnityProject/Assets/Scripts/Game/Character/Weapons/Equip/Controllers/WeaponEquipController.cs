using System;
using Core.Installers.Ids;
using Game.Character.Weapons.Equip.Models;
using Game.Weapons.Common.Models;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Game.Character.Weapons.Equip.Controllers
{
    public class WeaponEquipController : IInitializable, IDisposable
    {
        [Inject] private WeaponEquipModel WeaponEquipModel { get; }
        [Inject(Id = BindingIdentifiers.CharacterAimRig)] private Rig AimRig { get; }

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

        private void HandleOnWeaponEquipped(WeaponModel weaponModel)
        {
            SetRigAsWeaponEquipped();
        }

        private void SetRigAsWeaponEquipped()
        {
            AimRig.weight = 1.0f;
        }

        private void SetRigAsWeaponUnequipped()
        {
            AimRig.weight = 0f;
        }
    }
}
