using Game.Character.Highlight.Controllers;
using Zenject;

namespace Game.Character.Highlight.Installers
{
    public class ObjectHighlightInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ObjectHighlightController>()
                .AsSingle();
        }
    }
}
