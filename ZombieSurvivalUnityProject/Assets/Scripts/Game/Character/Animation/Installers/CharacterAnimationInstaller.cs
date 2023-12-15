using Core.Installers.Ids;
using Game.Character.Animation.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Character.Animation.Installers
{
    public class CharacterAnimationInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_animator)
                .WithId(BindingIdentifiers.CharacterAnimator)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterAnimationController>().AsSingle();
        }
    }
}
