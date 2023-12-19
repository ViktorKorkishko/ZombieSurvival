using UnityEngine;
using Zenject;

namespace Game.Weapons.Common.Installers
{
    public class WeaponViewInstaller : MonoInstaller
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        
        public override void InstallBindings()
        {
            Container.Bind<Collider>()
                .FromInstance(_collider);
            
            Container.Bind<Rigidbody>()
                .FromInstance(_rigidbody);
        }
    }
}
