using Game.Character.Movement.Locomotion.Controllers;
using Game.Character.Movement.Locomotion.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Locomotion.Installers
{
    public class CharacterLocomotionInstaller : MonoInstaller
    {
        [SerializeField] private CharacterLocomotionModel _characterLocomotionModel;
        
        [SerializeField] private CharacterController _characterController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterLocomotionModel).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterLocomotionController>().AsSingle();

            Container.BindInstance(_characterController).AsSingle();
        }
    }
}
