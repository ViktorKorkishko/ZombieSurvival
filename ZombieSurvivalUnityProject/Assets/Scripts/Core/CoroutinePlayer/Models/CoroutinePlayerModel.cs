using System;
using System.Collections;
using UnityEngine;

namespace Core.Coroutines.Models
{
    public class CoroutinePlayerModel
    {
        public Func<IEnumerator, int> OnCoroutineStarted { get; set; }
        public Action<int> OnCoroutineStopped { get; set; }

        public int StartCoroutine(IEnumerator enumerator)
        {
            if (OnCoroutineStarted != null)
            {
                return OnCoroutineStarted.Invoke(enumerator);
            }

            Debug.LogError($"Coroutine can not be ran.");
            return -1;
        }

        public void StopCoroutine(int coroutineIndex)
        {
            OnCoroutineStopped?.Invoke(coroutineIndex);
        }
    }
}
