using Game.InteractableObjects.Highlight.Controllers;
using Game.InteractableObjects.Highlight.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Highlight.Installers
{
    public class HighlightableObjectInstaller : MonoInstaller
    {
        [SerializeField] private Renderer _renderer;

        public override void InstallBindings()
        {
            Container.Bind<HighlightableObjectModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<HighlightableObjectController>().AsSingle();
            
            Container.BindInstance(_renderer).AsSingle();
        }
    }
}