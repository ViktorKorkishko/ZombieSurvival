#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Game.Stats.Health.Models
{
    public partial class HealthModel
    {
#if UNITY_EDITOR
        [ShowInInspector] 
        private float CurrentHealth => HealthStat?.CurrentValue ?? 0f;
        
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
#endif
    }
}
