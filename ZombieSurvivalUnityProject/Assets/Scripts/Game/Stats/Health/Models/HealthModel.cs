using System;
using Core.SaveSystem.Models;
using Core.SaveSystem.Saving.Common.Load;
using UnityEngine;

namespace Game.Stats.Health
{
    public class HealthModel : MonoSaveableModel<HealthModel.Data>
    {
        [Serializable]
        public new class Data
        {
            public float Health { get; set; }
        }
        
        public float Health => HealthStat.CurrentValue;
        public float MinHealth => HealthStat.MinValue;
        public float MaxHealth => HealthStat.MaxValue;
        
        private HealthStat HealthStat { get; set; }

        public event Action<float> OnHealthChanged;

        protected override string DataKey => "HealthModel.Data";
        
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

        [ContextMenu(nameof(Deal20))]
        public void Deal20()
        {
            AdjustHealth(-20);
        }
        
        [ContextMenu(nameof(Heal20))]
        public void Heal20()
        {
            AdjustHealth(20);
        }

        public void AdjustHealth(float value)
        {
            HealthStat.AdjustValue(value);
            OnHealthChanged?.Invoke(value);
        }
    }
}
