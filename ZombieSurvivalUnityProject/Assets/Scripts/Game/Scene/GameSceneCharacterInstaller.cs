using Game.Character.Facade;
using UnityEngine;
using Zenject;

public class GameSceneCharacterInstaller : MonoInstaller
{
    [SerializeField] private Transform _characterRoot;
    [SerializeField] private CharacterFacade _characterPrefab;
    
    public override void InstallBindings()
    {
        var characterInstance = Container
            .InstantiatePrefabForComponent<CharacterFacade>(_characterPrefab, _characterRoot);
        
        Container
        .Bind<CharacterFacade>()
        .FromInstance(characterInstance)
        .AsSingle();
    }
}
