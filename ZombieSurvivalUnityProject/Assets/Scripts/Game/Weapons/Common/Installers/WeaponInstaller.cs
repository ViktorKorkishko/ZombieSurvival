using Game.Weapons.Common.Controllers;
using Game.Weapons.Common.Models;
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
            Container.Bind<WeaponModel>()
                .AsSingle();
            
            Container.BindInstance(_weaponView)
                .AsSingle();
            
            Container.Bind<WeaponController>()
                .AsSingle();
        }
    }
}
