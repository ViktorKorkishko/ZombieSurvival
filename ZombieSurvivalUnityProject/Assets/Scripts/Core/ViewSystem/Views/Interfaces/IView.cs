using System;
using Core.ViewSystem.Enums;
using Core.ViewSystem.Views.Data;

namespace Core.ViewSystem.Views.Interfaces
{
    public interface IView
    {
        public ViewShowStatus ViewShowStatus { get; }

        Action OnShow { get; set; }
        Action<IView> OnHide { get; set; }

        void Show(IViewData viewData = null);
        void Hide();
    }
}
