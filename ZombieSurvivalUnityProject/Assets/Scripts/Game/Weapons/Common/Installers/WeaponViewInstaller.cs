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
            Container.BindInstance(_viewRoot).WithId(BindingIdentifiers.ViewRoot);
            Container.BindInstance(_collider);
            Container.BindInstance(_rigidbody);
        }
    }
}
