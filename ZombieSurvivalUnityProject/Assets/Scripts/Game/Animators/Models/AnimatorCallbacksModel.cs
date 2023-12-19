using System;

namespace Game.Animators.Models
{
    public class AnimatorCallbacksModel
    {
        public Action OnAnimatorMove { get; set; }

        public void TriggerOnAnimatorMove()
        {
            OnAnimatorMove?.Invoke();
        }
    }
}