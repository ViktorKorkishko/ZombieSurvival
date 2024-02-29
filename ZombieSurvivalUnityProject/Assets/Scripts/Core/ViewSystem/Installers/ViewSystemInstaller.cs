using Core.ViewSystem.Models;
using Core.ViewSystem.Providers;
using Core.ViewSystem.Providers.Layers;
using Core.ViewSystem.Providers.Views;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Installers
{
    public class ViewSystemInstaller : MonoInstaller
    {
        [SerializeField] private LayerProvider _layerProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ViewSystemModel>().AsSingle();
            
            Container
                .Bind<IViewProvider>()
                .To<ViewProvider>()
                .AsSingle();
            
            Container
                .Bind<ViewFactory>()
                .AsSingle();

            Container
                .Bind<LayerProvider>()
                .FromInstance(_layerProvider)
                .AsSingle();
        }
    }
}
