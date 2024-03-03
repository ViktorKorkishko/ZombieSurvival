using UnityEngine;
using Zenject;

namespace Game.Character.Facade.Installers
{
    public class CharacterFacadeInstaller : MonoInstaller
    {
        [SerializeField] private CharacterFacade _characterFacade;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CharacterFacade>()
                .FromInstance(_characterFacade)
                .AsSingle();
            
            _characterFacade.Init(Container);
        }
    }
}
