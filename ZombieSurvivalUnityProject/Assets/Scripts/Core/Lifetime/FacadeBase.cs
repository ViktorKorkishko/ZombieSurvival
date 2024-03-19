using UnityEngine;
using Zenject;

namespace Core.Lifetime
{
    public class FacadeBase : MonoBehaviour
    {
        protected DiContainer DiContainer { get; private set; }

        public void Initialize(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }
    }
}
