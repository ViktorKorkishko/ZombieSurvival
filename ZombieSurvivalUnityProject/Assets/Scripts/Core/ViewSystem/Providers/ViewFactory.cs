using Core.ViewSystem.Views;
using UnityEngine;
using Zenject;

namespace Core.ViewSystem.Providers
{
    public class ViewFactory : IFactory<ViewBase, ViewBase>
    {
        private Transform ViewParent { get; }

        public ViewFactory(Transform viewParent)
        {
            ViewParent = viewParent;
        }
        
        public ViewBase Create(ViewBase viewPrefab)
        {
            var viewInstance = Object.Instantiate(viewPrefab, ViewParent);
            return viewInstance;
        }
    }
}