using Core.Installers;
using Core.ViewSystem.Controllers;
using Core.ViewSystem.Views.Interfaces;
using Game.Stats.Health.Models;
using Game.Stats.Health.View;
using Zenject;

namespace Game.Stats.Health.Controllers
{
    public class HealthViewController : ViewControllerBase<HealthView>
    {
        [Inject(Id = BindingIdentifiers.PlayerHealthModel)] private HealthModel HealthModel { get; }

        public HealthViewController(IView view) : base(view) { }

        public override void Initialize()
        {
            base.Initialize();
            
            HealthModel.OnHealthChanged += HandleOnHealthChanged;
            
            View.SetMinHealth(HealthModel.MinHealth);
            View.SetMaxHealth(HealthModel.MaxHealth);
            View.SetCurrentHealth(HealthModel.Health);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            HealthModel.OnHealthChanged -= HandleOnHealthChanged;
        }

        private void HandleOnHealthChanged(float value)
        {
            View.SetCurrentHealth(HealthModel.Health);
        }
    }
}
