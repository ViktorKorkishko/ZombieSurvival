using Core.ViewSystem.Enums;
using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;

namespace Core.ViewSystem.Providers.Interfaces
{
    public interface IViewProvider
    {
        ViewBase RegisterView(ViewBase view, ViewId viewId, LayerId layerId);
        IView GetView(ViewId viewId);
        LayerId GetViewLayer(ViewId viewId);
    }
}
