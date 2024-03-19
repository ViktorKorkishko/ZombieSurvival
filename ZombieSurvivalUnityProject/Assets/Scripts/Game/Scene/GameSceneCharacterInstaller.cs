using Core.Lifetime;
using Game.Character.Facade;
using UnityEngine;
using Zenject;

namespace Game.Scene
{
    public class GameSceneCharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private FacadeBase _characterFacadePrefab;

        public override void InstallBindings()
        {
            var characterInstance = Container
                .InstantiatePrefabForComponent<CharacterFacade>(_characterFacadePrefab, _characterRoot);

            Container
                .Bind<CharacterFacade>()
                .FromInstance(characterInstance)
                .AsSingle();
        }
    }
}
