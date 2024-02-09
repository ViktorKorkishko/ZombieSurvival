using Core.ViewSystem.Views;
using Core.ViewSystem.Views.Interfaces;

namespace Core.ViewSystem.Providers
{
    public class ViewContainer
    {
        public ViewBase ViewPrefab { get; set; }
        public IView ViewInstance { get; set; }

        public ViewContainer(IView viewInstance)
        {
            ViewInstance = viewInstance;
        }
    }
}
