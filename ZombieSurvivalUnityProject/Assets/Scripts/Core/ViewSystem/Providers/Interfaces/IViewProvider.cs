using Core.ViewSystem.Enums;
using Core.ViewSystem.Views.Interfaces;

namespace Core.ViewSystem.Providers
{
    public interface IViewProvider
    {
        IView GetView(ViewId viewId);
    }
}
