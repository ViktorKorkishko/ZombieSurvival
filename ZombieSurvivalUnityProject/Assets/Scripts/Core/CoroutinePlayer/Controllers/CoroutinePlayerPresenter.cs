using System;
using System.Collections;
using Core.Coroutines.Models;
using Core.Coroutines.Views;
using Zenject;

namespace Core.Coroutines.Controllers
{
    public class CoroutinePlayerPresenter : IDisposable
    {
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }
        [Inject] private CoroutinePlayerView CoroutinePlayerView { get; }

        private int _currentIndex;

        public void Dispose()
        {
            DisposeCoroutines();
        }

        public int HandleOnCoroutineStarted(IEnumerator enumerator)
        {
            CoroutinePlayerModel.AddNewCoroutine(_currentIndex++, CoroutinePlayerView.StartCoroutine(enumerator));

            return _currentIndex;
        }

        public void StopCoroutine(int coroutineIndex)
        {
            CoroutinePlayerView.StopCoroutine(CoroutinePlayerModel.GetCoroutine(coroutineIndex));
        }

        private void HandleOnCoroutineStopped(int coroutineIndex)
        {
            StopCoroutine(coroutineIndex);
        }

        private void DisposeCoroutines()
        {
            // foreach (var keyValuePair in _indexToCoroutineDictionary)
            // {
            //     var coroutineIndex = keyValuePair.Key;
            //     StopCoroutine(coroutineIndex);
            // }
        }
    }
}
