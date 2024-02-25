using Core.ViewSystem.Models;
using Core.ViewSystem.Providers;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Installers
{
    public class ViewSystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _viewsParent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewSystemModel>().AsSingle();
            
            Container
                .Bind<IViewProvider>()
                .To<ViewProvider>()
                .AsSingle()
                .WithArguments(_viewsParent);
            
            Container
                .Bind<ViewFactory>()
                .AsSingle()
                .WithArguments(_viewsParent);
            
            // Container
            //     .Bind<Transform>()
            //     .WithId(_viewsParent)
            //     .FromInstance(_viewsParent)
            //     .AsCached();
        }
    }
}
