using UnityEngine;
using Zenject;

namespace Game.Weapons.Common.Installers
{
    public class WeaponIdInstaller : MonoInstaller
    {
        [SerializeField] private WeaponId _weaponId;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_weaponId);
        }
    }
}
