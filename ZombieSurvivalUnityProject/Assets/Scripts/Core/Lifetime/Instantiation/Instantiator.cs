using UnityEngine;
using Zenject;

namespace Core.Lifetime.Instantiation
{
    public class Instantiator
    {
        [Inject] private DiContainer DiContainer { get; }
        
        public GameObject InstantiatePrefab(GameObject gameObject, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            var prefabInstance = DiContainer.InstantiatePrefab(gameObject, position, rotation, parent);
            return prefabInstance;
        }
        
        public T InstantiatePrefabForComponent<T>(GameObject gameObject, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            var component = DiContainer.InstantiatePrefabForComponent<T>(gameObject, position, rotation, parent);
            return component;
        }
    }
}
