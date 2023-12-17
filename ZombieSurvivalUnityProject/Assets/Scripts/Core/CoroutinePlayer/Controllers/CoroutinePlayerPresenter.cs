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
        private const int _startIndex = 0;
        private int _currentIndex;

        public void Initialize()
        {
            CoroutinePlayerModel.OnCoroutineStarted += HandleOnCoroutineStarted;
            CoroutinePlayerModel.OnCoroutineStopped += OnCoroutineStopped;

            _currentIndex = _startIndex;
        }

        public void Dispose()
        {
            CoroutinePlayerModel.OnCoroutineStarted -= HandleOnCoroutineStarted;
            CoroutinePlayerModel.OnCoroutineStopped -= OnCoroutineStopped;

            DisposeCoroutines();
        }

        private void StopCoroutine(int coroutineIndex)
        {
            var coroutine = _indexToCoroutineDictionary[coroutineIndex];
            CoroutinePlayerView.StopCoroutine(coroutine);
            _indexToCoroutineDictionary.Remove(coroutineIndex);
        }

        private int HandleOnCoroutineStarted(IEnumerator enumerator)
        {
            var coroutine = CoroutinePlayerView.StartCoroutine(enumerator);
            _indexToCoroutineDictionary.Add(_currentIndex, coroutine);
            return _currentIndex++;
        }

        private void OnCoroutineStopped(int coroutineIndex)
        {
            StopCoroutine(coroutineIndex);
        }

        private void DisposeCoroutines()
        {
            foreach (var keyValuePair in _indexToCoroutineDictionary)
            {
                var coroutineIndex = keyValuePair.Key;
                StopCoroutine(coroutineIndex);
            }
        }
    }
}
