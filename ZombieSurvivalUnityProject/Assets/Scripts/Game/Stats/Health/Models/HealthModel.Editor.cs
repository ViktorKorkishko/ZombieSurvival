using UnityEngine;

namespace Game.Stats.Health.Models
{
    public partial class HealthModel
    {
#if UNITY_EDITOR
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
