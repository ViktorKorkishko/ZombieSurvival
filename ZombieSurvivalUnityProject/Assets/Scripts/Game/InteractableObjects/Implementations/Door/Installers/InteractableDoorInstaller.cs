using Core.Installers;
using Game.InteractableObjects.Common.Installers;
using Game.InteractableObjects.Implementations.Door.Controllers;
using Game.InteractableObjects.Implementations.Door.Models;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.Door.Installers
{
    public class InteractableDoorInstaller : InteractableObjectInstallerBase<InteractableDoorController>
    {
        [SerializeField] private InteractableDoorModel _interactableDoorModel;
        [SerializeField] private Rigidbody _rigidbody;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInterfacesAndSelfTo<InteractableDoorModel>().FromInstance(_interactableDoorModel).AsSingle();
            
            Container.BindInstance(_rigidbody).WithId(BindingIdentifiers.Rigidbody).AsSingle();
        }
    }
}
