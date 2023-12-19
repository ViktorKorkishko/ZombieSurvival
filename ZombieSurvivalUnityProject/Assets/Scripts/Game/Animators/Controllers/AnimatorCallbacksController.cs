using System;
using Game.Animators.Models;
using Game.Animators.Views;
using Zenject;

namespace Game.Animators.Controllers
{
    public class MonoBehaviourCallbacksController : IInitializable, IDisposable
    {
        [Inject] private AnimatorCallbacksView View { get; }
        [Inject] private AnimatorCallbacksModel Model { get; }

        void IInitializable.Initialize()
        {
            View.OnAnimatorMoveEvent += HandleOnAnimatorMove;
        }

        void IDisposable.Dispose()
        {
            View.OnAnimatorMoveEvent -= HandleOnAnimatorMove;
        }

        private void HandleOnAnimatorMove()
        {
            Model.TriggerOnAnimatorMove();
        }
    }
}
