using Game.Stats.Health.Models;
using UnityEngine;

namespace Game.Stats.Health.View
{
    public class HitboxView : MonoBehaviour
    {
        [field: SerializeField] public HealthModel HealthModel { get; private set; }
    }
}
