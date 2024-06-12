using System;

namespace Game.Stats.Core
{
    public abstract class BaseStat<T>
    {
        public T CurrentValue { get; protected set; }
        public T MinValue { get; }
        public T MaxValue { get; }

        public event Action<T> OnValueChanged;

        public BaseStat(T initialCurrentValue,T minValue, T maxValue)
        {
            CurrentValue = initialCurrentValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }
        
        public abstract void AdjustValue(T value);
        
        protected void InvokeValueChanged(T value)
        {
            OnValueChanged?.Invoke(value);
        }
    }
}
