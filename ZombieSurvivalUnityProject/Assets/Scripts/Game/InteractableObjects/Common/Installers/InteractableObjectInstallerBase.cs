using Game.InteractableObjects.Common.Enums;
using Game.InteractableObjects.Common.Models;
using Game.InteractableObjects.Common.Views;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Common.Installers
{
    public abstract class InteractableObjectInstallerBase<T> : MonoInstaller
    {
        [Header("Components")]
        [SerializeField] private InteractableObjectView _interactableObjectView;
        
        [Header("Object type")]
        [SerializeField] private InteractableObjectType _type;
        
        public override  void InstallBindings()
        {
            Container.Bind<InteractableObjectModel>().AsSingle();
            Container.BindInstance(_interactableObjectView).AsSingle();
            Container.BindInterfacesAndSelfTo<T>().AsSingle();
            
            Container.BindInstance(_type).AsSingle();
        }
    }
}
