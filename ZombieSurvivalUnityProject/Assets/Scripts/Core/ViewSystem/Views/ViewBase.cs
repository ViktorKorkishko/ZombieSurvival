using System;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using UnityEngine;

namespace Core.ViewSystem.Views
{
    public class ViewBase : MonoBehaviour, IView
    {
        public ViewShowStatus ViewShowStatus { get; private set; } = ViewShowStatus.None;
        
        public Action OnShow { get; set; }
        public Action<IView> OnHide { get; set; }
        
        public void Show(IViewData viewData = null)
        {
            gameObject.SetActive(true);

            ViewShowStatus = ViewShowStatus.Shown;
            OnShow?.Invoke();
            
            HandleOnShow(viewData);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
            
            ViewShowStatus = ViewShowStatus.Hidden;
            OnHide?.Invoke(this);
            
            HandleOnHide();
        }
        
        protected virtual void HandleOnShow(IViewData viewData) { }
        protected virtual void HandleOnHide() { }
    }
}
