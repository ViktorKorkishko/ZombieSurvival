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
            Container.BindInstance(_animator).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterAnimationController>().AsSingle();
        }
    }
}
