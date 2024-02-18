using Core.ViewSystem.Enums;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;

namespace Core.ViewSystem.Providers
{
    public interface IViewProvider
    {
        ViewBase RegisterView(ViewBase view, ViewId viewId);
        IView GetView(ViewId viewId);
    }
}
