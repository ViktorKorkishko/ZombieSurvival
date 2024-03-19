using Core.Lifetime;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class FacadeInstaller : MonoInstaller
    {
        [SerializeField] private FacadeBase _facadeBase;
        
        public override void InstallBindings()
        {
            _facadeBase.Initialize(Container);
        }
    }
}
