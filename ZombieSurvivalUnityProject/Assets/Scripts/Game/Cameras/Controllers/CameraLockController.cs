using System;
using System.Collections.Generic;
using System.Linq;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Models;
using Core.ViewSystem.Providers.Interfaces;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using Game.Cameras.Models;
using Zenject;

namespace Game.Cameras.Controllers
{
    public class CameraLockController : IInitializable, IDisposable
    {
        [Inject] private CameraModel CameraModel { get; }
        [Inject] private ViewSystemModel ViewSystemModel { get; }
        [Inject] private IViewProvider ViewProvider { get; }

        private Dictionary<IView, int> _lockSources = new();

        private readonly List<LayerId> _layersTriggeringLock = new()
        {
            LayerId.Windows, 
            LayerId.Popups,
        };

        void IInitializable.Initialize()
        {
            ViewSystemModel.OnViewShown += HandleOnViewShown;
            ViewSystemModel.OnViewHidden += HandleOnViewHidden;
        }

        void IDisposable.Dispose()
        {
            ViewSystemModel.OnViewShown -= HandleOnViewShown;
            ViewSystemModel.OnViewHidden -= HandleOnViewHidden;
        }

        private void HandleOnViewShown(IView view, ViewId viewId, IViewData data)
        {
            var viewLayer = ViewProvider.GetViewLayer(viewId);
            bool addLockSource = _layersTriggeringLock.Contains(viewLayer);
            if (!addLockSource)
                return;
            
            if (_lockSources.ContainsKey(view))
            {
                _lockSources[view] += 1;
            }
            else
            {
                _lockSources.Add(view, 1);
            }

            CameraModel.Lock();
        }

        private void HandleOnViewHidden(IView view)
        {
            if (_lockSources.TryGetValue(view, out var locksNumber))
            {
                _lockSources[view] -= 1;
            }
            else
            {
                _lockSources.Add(view, -1);
            }

            bool unlockCamera = _lockSources.Any(x => x.Value <= 0);
            if (unlockCamera)
            {
                CameraModel.Unlock();
            }
        }
    }
}
