using Core.SaveSystem.Entity;
using Game.Stats.Health.Controllers;
using Game.Stats.Health.Models;
using Game.Stats.Health.View;
using UnityEngine;
using Zenject;

namespace Game.Stats.Health.Installer
{
    public class HealthInstaller : MonoInstaller
    {
        [SerializeField] private HealthModel _healthModel;
        [SerializeField] private HitboxView _hitboxView;
        [SerializeField] private HealthBarView _healthBarView;
        
        [SerializeField] private SaveableEntity _saveableEntity;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_saveableEntity)
                .WhenInjectedIntoInstance(_healthModel);
            
            Container
                .BindInterfacesAndSelfTo<HealthModel>()
                .FromInstance(_healthModel)
                .AsCached();

            Container
                .BindInterfacesTo<HealthBarViewController>()
                .AsSingle()
                .WithArguments(_healthModel, _healthBarView);
        }
    }
}