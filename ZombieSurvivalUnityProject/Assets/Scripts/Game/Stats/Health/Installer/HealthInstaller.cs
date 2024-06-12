using Core.SaveSystem.Entity;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers.Interfaces;
using Game.Stats.Health.Controllers;
using Game.Stats.Health.View;
using UnityEngine;
using Zenject;

namespace Game.Stats.Health.Installer
{
    public class HealthInstaller : MonoInstaller
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private HealthModel _healthModel;
        [SerializeField] private HitboxView _hitboxView;
        [SerializeField] private SaveableEntity _saveableEntity;
        
        [Inject] private IViewProvider ViewProvider { get; }
        
        public override void InstallBindings()
        {          
            Container
                .BindInstance(_saveableEntity)
                .WhenInjectedInto<HealthModel>();

            Container
                .Bind<HealthModel>()
                .WhenInjectedInto<HitboxView>();
            
            Container
                .BindInterfacesAndSelfTo<HealthModel>()
                .FromInstance(_healthModel)
                .AsCached()
                .WithArguments(_saveableEntity);
            
            var viewInstance = ViewProvider.RegisterView(_healthView, ViewId.Health, LayerId.HUD, true, false);
            
            Container
                .BindInterfacesTo<HealthViewController>()
                .AsSingle()
                .WithArguments(viewInstance);
        }
    }
}
