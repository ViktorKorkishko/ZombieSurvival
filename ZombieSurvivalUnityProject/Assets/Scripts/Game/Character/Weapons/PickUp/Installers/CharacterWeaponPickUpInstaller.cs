using Game.Character.Weapons.PickUp.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Weapons.PickUp.Installers
{
    public class CharacterWeaponPickUpInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterWeaponPickUpModel>().AsSingle();
        }
    }
}
