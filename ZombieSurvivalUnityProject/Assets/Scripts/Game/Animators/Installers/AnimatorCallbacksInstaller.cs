using Game.Animators.Controllers;
using Game.Animators.Models;
using Game.Animators.Views;
using UnityEngine;
using Zenject;

namespace Game.Animators.Installers
{
    public class AnimatorCallbacksInstaller : MonoInstaller
    {
        [SerializeField] private AnimatorCallbacksView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<AnimatorCallbacksModel>().AsSingle();
            Container.BindInstance(_view).AsSingle();
            Container.BindInterfacesAndSelfTo<MonoBehaviourCallbacksController>().AsSingle();
        }
    }
}
