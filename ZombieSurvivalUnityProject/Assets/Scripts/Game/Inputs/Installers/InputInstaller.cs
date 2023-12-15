using Game.Inputs.Controllers;
using Game.Inputs.Models;
using Zenject;

namespace Game.Inputs.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
        }
    }
}
