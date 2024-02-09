using System.Collections.Generic;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Views.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Providers
{
    public class ViewProvider : IViewProvider
    {
        private Dictionary<ViewId, IView> _viewIdToViewInstanceDictionary;

        [Inject]
        public ViewProvider(Dictionary<ViewId, IView> viewIdToViewInstanceDictionary)
        {
            _viewIdToViewInstanceDictionary = viewIdToViewInstanceDictionary;
        }

        public IView GetView(ViewId viewId)
        {
            if (_viewIdToViewInstanceDictionary.TryGetValue(viewId, out var _viewInstance))
            {
                return _viewInstance;
            }
            
            Debug.Log(new KeyNotFoundException($"View with [{viewId}] is not binded!"));
            return null;
        }
    }
}