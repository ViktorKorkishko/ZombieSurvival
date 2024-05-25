using Core.ViewSystem.Enums;
using Core.ViewSystem.Views.Interfaces;

namespace Core.ViewSystem.Providers.Data
{
    public class ViewData
    {
        public IView ViewInstance { get; }
        public ViewId ViewId { get; }
        public LayerId LayerId { get; }
        
        public ViewData(IView viewInstance, ViewId viewId, LayerId layerId)
        {
            ViewInstance = viewInstance;
            ViewId = viewId;
            LayerId = layerId;
        }
    }
}
