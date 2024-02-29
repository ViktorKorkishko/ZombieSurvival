using System;
using System.Collections.Generic;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Providers.Views
{
    public class ViewProvider : IViewProvider
    {
        [Inject] private ViewFactory ViewFactory { get; }

        private Dictionary<ViewId, IView> _viewIdToViewInstanceDictionary = new();
        
        public ViewBase RegisterView(ViewBase viewPrefab, ViewId viewId, LayerId layerId)
        {
            var viewInstance = ViewFactory.Create(viewPrefab, layerId);
            
            if (!_viewIdToViewInstanceDictionary.TryAdd(viewId, viewInstance))
            {
                Debug.LogException(new ArgumentException($"View with ViewId [{viewId}] is already registered!"));
            }
            
            return viewInstance;
        }

        public IView GetView(ViewId viewId)
        {
            if (_viewIdToViewInstanceDictionary.TryGetValue(viewId, out var viewInstance))
            {
                return viewInstance;
            }
            
            Debug.Log(new KeyNotFoundException($"View with [{viewId}] is not binded!"));
            return null;
        }
    }
}
