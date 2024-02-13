using System;
using Core.Installers;
using Game.InteractableObjects.Highlight.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Highlight.Controllers
{
    public class HighlightableObjectController : IInitializable, IDisposable
    {
        [Inject] private HighlightableObjectModel Model { get; }
        [Inject(Id = BindingIdentifiers.Renderer)] private Renderer Renderer { get; }

        void IInitializable.Initialize()
        {
            Model.OnGetRenderer += HandleOnGetRenderer;
        }

        void IDisposable.Dispose()
        {
            Model.OnGetRenderer -= HandleOnGetRenderer;
        }

        private Renderer HandleOnGetRenderer()
        {
            return Renderer;
        }
    }
}
