using Core.Installers;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Common.Installers
{
    public class WeaponViewInstaller : MonoInstaller
    {
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(BindingIdentifiers.ViewRoot).FromInstance(_viewRoot);
            Container.Bind<Collider>().FromInstance(_collider);
            Container.Bind<Rigidbody>().FromInstance(_rigidbody);
        }
    }
}
