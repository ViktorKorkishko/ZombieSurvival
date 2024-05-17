using System;
using UnityEngine;
using Zenject;

namespace Core.Lifetime.Facade
{
    public abstract class FacadeBase : MonoBehaviour
    {
        protected DiContainer DiContainer { get; private set; }

        public void SetDiContainer(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }
    }

    public abstract class FacadeBase<T> : FacadeBase
    {
        [field: SerializeField] public T Data { get; private set; }

        public abstract void InitData(T data);
    }

    #region Inner types

    [Serializable]
    public abstract class FacadeDataBase
    {
    }

    #endregion
}
