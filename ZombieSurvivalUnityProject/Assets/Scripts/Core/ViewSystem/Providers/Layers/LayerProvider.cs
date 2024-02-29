using System;
using System.Collections.Generic;
using System.Linq;
using Core.ViewSystem.Enums;
using UnityEngine;

namespace Core.ViewSystem.Providers.Layers
{
    public class LayerProvider : MonoBehaviour
    {
        [SerializeField] private List<LayerData> _layersData;

        public bool TryGetLayer(LayerId layerId, out Transform layerTransform)
        {
            var layer = _layersData.FirstOrDefault(x => x.Id == layerId);
            if (layer != null)
            {
                layerTransform = layer.LayerTransform;
                return true;
            }

            Debug.LogException(new NullReferenceException($"Layer with id [{layerId}] is not found!"));
            layerTransform = null;
            return false;
        }
    }

    [Serializable]
    public class LayerData
    {
        [field: SerializeField] public Transform LayerTransform { get; private set; }
        [field: SerializeField] public LayerId Id { get; private set; }
    }
}
