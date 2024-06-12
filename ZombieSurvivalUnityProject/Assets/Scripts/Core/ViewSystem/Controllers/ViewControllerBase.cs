using System;
using Core.ViewSystem.Views.Interfaces;
using Zenject;

namespace Core.ViewSystem.Controllers
{
    public class ViewControllerBase<T> : IInitializable, IDisposable
        where T : IView
    {
        protected T View => (T)_view;
        
        private IView _view;
        
        public ViewControllerBase(IView view)
        {
            _view = view;
        }
        
        public virtual void Initialize()
        {
            _view.OnShow += HandleOnShow;
            _view.OnHide += HandleOnHide;
        }

        public virtual void Dispose()
        {
            _view.OnShow -= HandleOnShow;
            _view.OnHide -= HandleOnHide;
        }

        protected virtual void HandleOnShow() { }
        protected virtual void HandleOnHide(IView view) { }
    }
}
