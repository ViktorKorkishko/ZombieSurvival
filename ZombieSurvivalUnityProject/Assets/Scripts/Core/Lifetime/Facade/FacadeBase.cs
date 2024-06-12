using Game.Items.Data;
using UnityEngine;
using Zenject;

namespace Core.Lifetime.Facade
{
    public abstract class FacadeBase : MonoBehaviour
    {
        protected DiContainer LocalDiContainer { get; private set; }

        public abstract void Init(ItemData data);
        
        public void InitDiContainer(DiContainer diContainer)
        {
            LocalDiContainer = diContainer;
        }
    }
}
