using Zenject;
using UnityEngine;
using Core.Coroutines.Models;
using Core.Coroutines.Controllers;
using Core.Coroutines.Views;

namespace Core.Coroutines.Installers
{
    public class CoroutinePlayerInstaller : MonoInstaller
    {
        [SerializeField] private CoroutinePlayerView _view;

        public override void InstallBindings()
        {
            Container.BindInstance(_view).AsSingle();
            Container.Bind<CoroutinePlayerModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoroutinePlayerPresenter>().AsSingle();
        }
    }
}
