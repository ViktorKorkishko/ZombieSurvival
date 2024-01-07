using System.Collections.Generic;
using System.Linq;
using Game.Weapons.Common;
using UnityEngine;

namespace Game.Animations
{
    [CreateAssetMenu(fileName = "WeaponsAnimatorStatesNamesProvider", menuName = "Editor/Animator/WeaponsAnimatorStatesNamesProvider")]
    public class WeaponsAnimatorStatesNamesProvider : ScriptableObject
    {
        [SerializeField] private List<WeaponAnimationsContainer> _weaponsAnimationsContainer;

        public bool TryGetWeaponAnimationsContainer(WeaponId weaponId, out WeaponAnimationsContainer weaponAnimationsContainer)
        { 
            weaponAnimationsContainer = _weaponsAnimationsContainer.FirstOrDefault(x => x.WeaponId == weaponId);
            if (weaponAnimationsContainer == null)
            {
                Debug.LogError(
                    new KeyNotFoundException($"WeaponAnimationsContainer for {weaponId} is not found."));
                return false;
            }

            return true;
        }
    }
}
