using Core.Lifetime.Facade;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class BaseFacadeInstaller : MonoInstaller
    {
        [SerializeField] private FacadeBase _facadeBase;
        
        public override void InstallBindings()
        {
            _facadeBase.SetDiContainer(Container);
        }
    }
}
