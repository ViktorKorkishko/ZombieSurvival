using Core.ViewSystem.Enums;
using Core.ViewSystem.Providers;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using Zenject;

namespace Core.ViewSystem.Models
{
    public class ViewSystemModel
    {
        [Inject] private IViewProvider ViewProvider { get; }
        
        public IView Show(ViewId viewId, IViewData data = null)
        {
            var view = ViewProvider.GetView(viewId);
            view.Show(data);
            return view;
        }
    }
}
