using Core.Installers;
using Core.SaveSystem.Entity;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers.Interfaces;
using Game.Stats.Health.Controllers;
using Game.Stats.Health.Models;
using Game.Stats.Health.View;
using UnityEngine;
using Zenject;

namespace Game.Stats.Health.Installer
{
    public class PlayerHealthInstaller : MonoInstaller
    {
        [Header("Health system")]
        [SerializeField] private HealthView _healthView;
        [SerializeField] private HealthModel _healthModel;
        
        [Header("Hitbox")]
        [SerializeField] private HitboxView _hitboxView;
        
        [Header("Save")]
        [SerializeField] private SaveableEntity _saveableEntity;
        
        [Inject] private IViewProvider ViewProvider { get; }
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_saveableEntity)
                .WhenInjectedIntoInstance(_healthModel);;
            
            Container
                .Bind<HealthModel>()
                .WithId(BindingIdentifiers.PlayerHealthModel)
                .FromInstance(_healthModel)
                .AsCached();

            Container
                .BindInterfacesTo<HealthModel>()
                .FromInstance(_healthModel)
                .AsCached();
            
            var viewInstance = ViewProvider.RegisterView(_healthView, ViewId.Health, LayerId.HUD, true, false);
            
            Container
                .BindInterfacesTo<HealthViewController>()
                .AsSingle()
                .WithArguments(viewInstance);
        }
    }
}
