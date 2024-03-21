using Game.InteractableObjects.Highlight.Models;
using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Highlight.Views
{
    public class HighlightableObjectView : MonoBehaviour
    {
        [SerializeField] private Context _context;

        public Renderer Renderer => _renderer ??= _context.Container.Resolve<HighlightableObjectModel>().GetRenderer();
        
        private Renderer _renderer;
    }
}
