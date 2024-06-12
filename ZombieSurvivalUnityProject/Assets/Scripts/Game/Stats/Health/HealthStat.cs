using Game.Stats.Core;

namespace Game.Stats.Health
{
    public class HealthStat : BaseStat<float>
    {
        public override void AdjustValue(float value)
        {
            CurrentValue += value;
            InvokeValueChanged(value);
        }

        public HealthStat(float initialCurrentValue, float minValue, float maxValue)
            : base(initialCurrentValue, minValue, maxValue)
        {
            
        }
    }
}
