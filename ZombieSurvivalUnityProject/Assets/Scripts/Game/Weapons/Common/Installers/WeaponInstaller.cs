using Game.Weapons.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Common.Installers
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private WeaponView _weaponView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_weaponView).AsSingle();
        }
    }
}
