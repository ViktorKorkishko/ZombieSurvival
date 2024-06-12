using Core.ViewSystem.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Stats.Health.View
{
    public class HealthView : ViewBase
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _currentHealthValueText;
        
        public void SetCurrentHealth(float value)
        {
            _slider.value = value;
            _currentHealthValueText.text = value.ToString();
        }
        
        public void SetMinHealth(float value)
        {
            _slider.minValue = value;
        }
        
        public void SetMaxHealth(float value)
        {
            _slider.maxValue = value;
        }
    }
}
