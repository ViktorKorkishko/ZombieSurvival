using System.Collections.Generic;
using UnityEngine;

namespace Core.Coroutines.Models
{
    public class CoroutinePlayerModel
    {
        private readonly Dictionary<int, Coroutine> coroutinesHash = new();

        public void AddNewCoroutine(int coroutineIndex, Coroutine coroutine)
        {
            coroutinesHash.Add(coroutineIndex, coroutine);
        }

        public Coroutine GetCoroutine(int coroutineIndex)
        {
            return coroutinesHash.TryGetValue(coroutineIndex, out var coroutineFromHash) ? coroutineFromHash : null;
        }
    }
}
