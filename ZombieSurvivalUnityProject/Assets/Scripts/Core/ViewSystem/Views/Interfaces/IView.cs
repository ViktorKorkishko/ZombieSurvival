using System;
using Core.ViewSystem.Views.Data;

namespace Core.ViewSystem.Views.Interfaces
{
    public interface IView
    {
        Action OnShow { get; set; }
        Action OnHide { get; set; }

        void Show(IViewData viewData = null);
        void Hide();
    }
}