using System;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;
using Core.SaveSystem.Saving.Common.Load;

namespace Game.Stats.Health.Models
{
    public partial class HealthModel : MonoSaveableModel<HealthModel.Data>
    {
        [Serializable]
        public new class Data
        {
            public float Health { get; set; }
        }
        
        public int Health => (int)HealthStat?.CurrentValue;
        public float MinHealth => HealthStat.MinValue;
        public float MaxHealth => HealthStat.MaxValue;
        
        private HealthStat HealthStat { get; set; }
        
        public event Action<float> OnHealthChanged;
        
        protected override string DataKey => "HealthModel.Data";
        
        public override void Construct(SaveableEntity entity)
        {
            base.Construct(entity);

            HealthStat = new HealthStat(100f, 0f, 100f);
        }
        
        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    float initialValue = loadResult.Data.Health;
                    HealthStat = new HealthStat(initialValue, 0f, 100f);
                    return;
                
                default:
                    HealthStat = new HealthStat(100f, 0f, 100f);
                    break;
            }
        }
        
        protected override void HandleOnDataPreSaved()
        {
            base.Data.Health = HealthStat.CurrentValue;
        }

        public void Restore()
        {
            var adjustValue = MaxHealth - Health;
            AdjustHealth(adjustValue);
        }

        public void AdjustHealth(float value)
        {
            HealthStat.AdjustValue(value);
            OnHealthChanged?.Invoke(value);
        }
    }
}
