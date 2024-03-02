using Core.Installers;
using Game.Character.Movement.Position.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Position.Installers
{
    public class CharacterPositionInstaller : MonoInstaller
    {
        [SerializeField] private Transform _viewRootTransform;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CharacterPositionModel>().AsSingle();
            
            Container
                .BindInstance(_viewRootTransform)
                .WithId(BindingIdentifiers.ViewRoot);
        }
    }
}
