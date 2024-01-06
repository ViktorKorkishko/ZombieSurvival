using Core.Installers;
using Game.Character.Animation.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Character.Animation.Installers
{
    public class CharacterAnimationInstaller : MonoInstaller
    {
        [SerializeField] private Animator _characterLocomotionAnimator;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterLocomotionAnimator)
                .WithId(BindingIdentifiers.CharacterLocomotionAnimator);
            Container.BindInterfacesAndSelfTo<CharacterAnimationController>().AsSingle();
        }
    }
}
