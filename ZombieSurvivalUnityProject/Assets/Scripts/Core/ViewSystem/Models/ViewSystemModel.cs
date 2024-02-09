using System;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using Zenject;

namespace Core.ViewSystem.Models
{
    public class ViewSystemModel : IInitializable, IDisposable
    {
        [Inject] private IViewProvider ViewProvider { get; }

        void IInitializable.Initialize()
        {
            
        }

        void IDisposable.Dispose()
        {
            
        }
        
        public IView Show(ViewId viewId, IViewData data = null)
        {
            var view = ViewProvider.GetView(viewId);
            view.Show(data);
            return view;
        }
    }
}
