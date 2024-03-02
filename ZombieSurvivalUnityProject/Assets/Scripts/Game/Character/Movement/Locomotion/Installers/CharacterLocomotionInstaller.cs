using Core.Installers;
using Game.Character.Movement.Locomotion.Controllers;
using Game.Character.Movement.Locomotion.Models;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Character.Movement.Locomotion.Installers
{
    public class CharacterLocomotionInstaller : MonoInstaller
    {
        [SerializeField] private CharacterLocomotionModel _characterLocomotionModel;
        
        [Header("Components")]
        [SerializeField] private CharacterController _characterController;

        [Header("Animation")] 
        [SerializeField] private string _jumpingAnimationParamName;
        [SerializeField] private string _sprintingAnimationParamName;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterLocomotionModel).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterLocomotionController>().AsSingle();

            Container.BindInstance(_characterController).AsSingle();
            Container.BindInstance(_jumpingAnimationParamName).WithId(BindingIdentifiers.JumpParamId);
            Container.BindInstance(_sprintingAnimationParamName).WithId(BindingIdentifiers.SprintParamId);
        }
    }
}
