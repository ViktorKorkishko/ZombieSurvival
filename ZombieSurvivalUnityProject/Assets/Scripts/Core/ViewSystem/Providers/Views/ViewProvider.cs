using System;
using System.Collections.Generic;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers.Data;
using Core.ViewSystem.Providers.Interfaces;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Providers.Views
{
    public class ViewProvider : IViewProvider
    {
        [Inject] private ViewFactory ViewFactory { get; }

        private Dictionary<ViewId, ViewData> _viewIdToViewInstanceDictionary = new();
        
        public ViewBase RegisterView(ViewBase viewPrefab, ViewId viewId, LayerId layerId)
        {
            var viewInstance = ViewFactory.Create(viewPrefab, layerId);
            
            if (!_viewIdToViewInstanceDictionary.TryAdd(viewId, new ViewData(viewInstance, viewId, layerId)))
            {
                Debug.LogException(new ArgumentException($"View with ViewId [{viewId}] is already registered!"));
            }
            
            return viewInstance;
        }

        public IView GetView(ViewId viewId)
        {
            if (_viewIdToViewInstanceDictionary.TryGetValue(viewId, out var viewData))
            {
                return viewData.ViewInstance;
            }
            
            Debug.Log(new KeyNotFoundException($"View with [{viewId}] is not bound!"));
            return null;
        }

        public LayerId GetViewLayer(ViewId viewId)
        {
            if (_viewIdToViewInstanceDictionary.TryGetValue(viewId, out var viewData))
            {
                return viewData.LayerId;
            }
            
            Debug.Log(new KeyNotFoundException($"View with [{viewId}] is not bound!"));
            return LayerId.None;
        }
    }
}
