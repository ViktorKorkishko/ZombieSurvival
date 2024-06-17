using Game.Stats.Health.View;
using UnityEngine;

namespace Game._TEMP_
{
    public class Damager : MonoBehaviour
    {
        [Range(-5, 5)] [SerializeField] private float _damage;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<HitboxView>(out var hitboxView))
            {
                var adjustValue = -_damage * Time.deltaTime;
                hitboxView.HealthModel.AdjustHealth(adjustValue);
            }
        }
    }
}
