using Core.ViewSystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Stats.Health.View
{
    public class HealthBarView : ViewBase
    {
        [SerializeField] private Slider _healthSlider;

        public void SetHealth(float value)
        {
            _healthSlider.value = value;
        }
        
        public void SetMinHealth(float value)
        {
            _healthSlider.minValue = value;
        }
        
        public void SetMaxHealth(float value)
        {
            _healthSlider.maxValue = value;
        }
    }
}
