using System.Collections.Generic;
using Core.Lifetime;
using UnityEngine;
using Zenject;

namespace Game.Scene
{
    public class GameSceneCharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private FacadeBase _character;

        [Header("Sub Installers")] 
        [SerializeField] private List<MonoInstaller> _playerSubInstallers;

        public override void InstallBindings()
        {
            _character.transform.SetParent(_characterRoot);
            
            // _playerSubInstallers.ForEach(x => x.InstallBindings());
        }
    }
}
