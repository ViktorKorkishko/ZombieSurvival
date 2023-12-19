using System;
using UnityEngine;

namespace Game.Animators.Views
{
    public class AnimatorCallbacksView : MonoBehaviour
    {
        public Action OnAnimatorMoveEvent { get; set; }

        private void OnAnimatorMove()
        {
            OnAnimatorMoveEvent?.Invoke();
        }
    }
}