using System;
using System.Collections;
using System.Collections.Generic;
using Core.Coroutines.Models;
using Core.Coroutines.Views;
using UnityEngine;
using Zenject;

namespace Core.Coroutines.Controllers
{
    public class CoroutinePlayerPresenter : IInitializable, IDisposable
    {
        [Inject] private CoroutinePlayerModel CoroutinePlayerModel { get; }
        [Inject] private CoroutinePlayerView CoroutinePlayerView { get; }

        private readonly Dictionary<int, Coroutine> _indexToCoroutineDictionary = new();
        private int _currentIndex = 0;

        public void Initialize()
        {
            CoroutinePlayerModel.OnCoroutineStarted += HandleOnCoroutineStarted;
            CoroutinePlayerModel.OnCoroutineStopped += HandleOnCoroutineStopped;
        }

        public void Dispose()
        {
            CoroutinePlayerModel.OnCoroutineStarted -= HandleOnCoroutineStarted;
            CoroutinePlayerModel.OnCoroutineStopped -= HandleOnCoroutineStopped;

            DisposeCoroutines();
        }

        private int HandleOnCoroutineStarted(IEnumerator enumerator)
        {
            var coroutine = CoroutinePlayerView.StartCoroutine(enumerator);
            _indexToCoroutineDictionary.Add(_currentIndex, coroutine);
            return _currentIndex++;
        }

        private void StopCoroutine(int coroutineIndex)
        {
            if (_indexToCoroutineDictionary.TryGetValue(coroutineIndex, out var coroutine))
            {
                CoroutinePlayerView.StopCoroutine(coroutine);
                _indexToCoroutineDictionary.Remove(coroutineIndex);
            }
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
