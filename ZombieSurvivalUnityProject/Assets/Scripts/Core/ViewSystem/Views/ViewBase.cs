using System;
using Core.ViewSystem.Views.Data;
using Core.ViewSystem.Views.Interfaces;
using UnityEngine;

namespace Core.ViewSystem.Views
{
    public class ViewBase : MonoBehaviour, IView
    {
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(IViewData viewData = null)
        {
            gameObject.SetActive(true);
            
            OnShow?.Invoke();
            
            HandleOnShow(viewData);
        }

        public void Hide()
        {
            OnHide?.Invoke();
            
            HandleOnHide();
        }

        protected virtual void HandleOnShow(IViewData viewData) { }
        protected virtual void HandleOnHide() { }
    }
}
