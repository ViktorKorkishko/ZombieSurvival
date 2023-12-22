using UnityEngine;
using Zenject;

namespace Game.InteractableObjects.Common.Views
{
    public class InteractableObjectView : MonoBehaviour
    {
        [SerializeField] private Context _context;

        public Context Context => _context;
    }
}
