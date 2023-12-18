using Core.Installers.Ids;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Grips.Installer
{
    public class GripsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _leftHandGripTansform;
        [SerializeField] private Transform _rightHandGripTansform;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.LeftHandGripTransform)
                .FromInstance(_leftHandGripTansform);
            Container.Bind<Transform>()
                .WithId(BindingIdentifiers.RightHandGripTransform)
                .FromInstance(_rightHandGripTansform);
        }
    }
}
