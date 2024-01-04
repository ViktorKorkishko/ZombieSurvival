using Core.Installers;
using Game.Character.Movement.Locomotion.Controllers;
using Game.Character.Movement.Locomotion.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Locomotion.Installers
{
    public class CorrectGravityCharacterLocomotionInstaller : MonoInstaller
    {
        [SerializeField] private CorrectGravityCharacterLocomotionModel correctGravityCharacterLocomotionModel;
        
        [Header("Components")]
        [SerializeField] private CharacterController _characterController;

        [Header("Animation")] 
        [SerializeField] private string _jumpingAnimationParamName;
        
        public override void InstallBindings()
        {
            Container.BindInstance(correctGravityCharacterLocomotionModel).AsSingle();
            Container.BindInterfacesAndSelfTo<CorrectGravityCharacterLocomotionController>().AsSingle();

            Container.BindInstance(_characterController).AsSingle();
            Container.BindInstance(_jumpingAnimationParamName).WithId(BindingIdentifiers.JumpParamId);
        }
    }
}
