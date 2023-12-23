using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Highlight.Views
{
    public class HighlightableObjectView : MonoBehaviour
    {
        [SerializeField] private Context _context;

        public Context Context => _context;
    }
}
