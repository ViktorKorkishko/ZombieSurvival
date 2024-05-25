using System;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers.Interfaces;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using Zenject;

namespace Core.ViewSystem.Models
{
    public class ViewSystemModel
    {
        [Inject] private IViewProvider ViewProvider { get; }
        
        public event Action<IView, ViewId, IViewData> OnViewShown;
        public event Action<IView> OnViewHidden;
        
        public IView Show(ViewId viewId, IViewData data = null)
        {
            var view = ViewProvider.GetView(viewId);
            view.Show(data);

            view.OnHide += HandleOnViewHidden;
            
            OnViewShown?.Invoke(view, viewId, data);
            return view;
        }
        
        private void HandleOnViewHidden(IView view)
        {
            view.OnHide -= HandleOnViewHidden;
            
            OnViewHidden?.Invoke(view);
        }
    }
}
