using Core.Lifetime.Facade;
using UnityEngine;
using Zenject;

namespace Game.Scene
{
    public class GameSceneCharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private FacadeBase _character;
        
        public override void InstallBindings()
        {
            _character.transform.SetParent(_characterRoot);
        }
    }
}
