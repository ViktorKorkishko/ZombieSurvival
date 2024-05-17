using Core.Lifetime.Facade;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class FacadeInstaller<TFacade, TData> : MonoInstaller 
        where TFacade : FacadeBase<TData> 
    {
        [SerializeField] private TFacade _facade;

        public override void InstallBindings()
        {
            _facade.SetDiContainer(Container);
        }
    }
}
