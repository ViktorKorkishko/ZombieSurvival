using UnityEngine;
using Zenject;

namespace Core.Lifetime.Facade.Installer
{
    public class BaseFacadeInstaller : MonoInstaller
    {
        [SerializeField] private FacadeBase _facadeBase;
        
        public override void InstallBindings()
        {
            _facadeBase.InitDiContainer(Container);
        }
    }
}
