using System.Collections;
using Core.Coroutines.Models;
using Core.ViewSystem.Controllers;
using Game.Stats.Health.Models;
using Game.Stats.Health.View;
using UnityEngine;
using Zenject;

namespace Game.Stats.Health.Controllers
{
    public class HealthBarViewController : ViewControllerBase<HealthBarView>
    {
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }

        private HealthModel HealthModel { get; }

        private int _viewShowCoroutineIndex = -1;

        public HealthBarViewController(HealthModel healthModel, HealthBarView healthBarView) 
            : base(healthBarView)
        {
            HealthModel = healthModel;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            HealthModel.OnHealthChanged += HandleOnHealthChanged;
            
            View.SetMinHealth(HealthModel.MinHealth);
            View.SetMaxHealth(HealthModel.MaxHealth);
            View.SetHealth(HealthModel.Health);
            
            View.Hide();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            HealthModel.OnHealthChanged -= HandleOnHealthChanged;
        }
        
        private void HandleOnHealthChanged(float adjustValue)
        {
            var currentHealth = HealthModel.Health;
            View.SetHealth(currentHealth);
            
            CoroutinePlayerModel.StopCoroutine(_viewShowCoroutineIndex);
            _viewShowCoroutineIndex = CoroutinePlayerModel.StartCoroutine(ShowHealthBar());
                
            if (currentHealth <= 0)
            {
                HealthModel.Restore();
            }
        }

        private IEnumerator ShowHealthBar()
        {
            View.Show();
            yield return new WaitForSeconds(1.5f);
            View.Hide();
        }
    }
}