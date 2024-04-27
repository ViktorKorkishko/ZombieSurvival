using UnityEngine;
using Zenject;

namespace Core.Lifetime
{
    public class Instantiator
    {
        [Inject] private DiContainer DiContainer { get; }

        public T Instantiate<T>(GameObject gameObject, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            var component = DiContainer.InstantiatePrefabForComponent<T>(gameObject, position, rotation, parent);
            return component;
        }
    }
}
