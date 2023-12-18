using Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Equip.Installer
{
    public class EquipInstaller : MonoInstaller
    {
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private Transform _leftHandGripTansform;
        [SerializeField] private Transform _rightHandGripTansform;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.Root)
                .FromInstance(_weaponRoot);
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.LeftHandGripTransform)
                .FromInstance(_leftHandGripTansform);
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.RightHandGripTransform)
                .FromInstance(_rightHandGripTansform);
        }
    }
}
