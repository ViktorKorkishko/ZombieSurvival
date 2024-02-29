using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers.Layers;
using Core.ViewSystem.Views;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Providers.Views
{
    public class ViewFactory : IFactory<ViewBase, LayerId, ViewBase>
    {
        [Inject] private LayerProvider LayerProvider { get; }

        public ViewBase Create(ViewBase viewPrefab, LayerId layerId)
        {
            if (LayerProvider.TryGetLayer(layerId, out var layerTransform))
            {
                var viewInstance = Object.Instantiate(viewPrefab, layerTransform);
                return viewInstance;
            }

            return null;
        }
    }
}