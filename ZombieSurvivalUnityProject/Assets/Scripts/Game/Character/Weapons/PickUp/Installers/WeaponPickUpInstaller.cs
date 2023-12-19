using Game.Character.Weapons.PickUp.Controllers;
using Game.Character.Weapons.PickUp.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.PickUp.Installers
{
    public class WeaponPickUpInstaller : MonoInstaller
    {
        [SerializeField] private WeaponPickUpModel _weaponPickUpModel;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_weaponPickUpModel).AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponPickUpController>().AsSingle();
        }
    }
}
