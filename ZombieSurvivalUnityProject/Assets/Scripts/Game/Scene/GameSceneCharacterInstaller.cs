using UnityEngine;
using Zenject;

public class GameSceneCharacterInstaller : MonoInstaller
{
    [SerializeField] private Transform _characterRoot;
    [SerializeField] private GameObject _characterPrefab;
    
    public override void InstallBindings()
    {
        var characterInstance = Container.InstantiatePrefab(_characterPrefab, _characterRoot);
    }
}
