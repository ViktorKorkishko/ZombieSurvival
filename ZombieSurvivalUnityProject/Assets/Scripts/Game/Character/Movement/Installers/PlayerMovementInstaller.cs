using Game.Character.Movement.Controllers;
using Game.Character.Movement.Models;
using Zenject;

namespace Game.Character.Movement.Installers
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterMovementModel>().AsSingle();
            Container.Bind<CharacterMovementController>().AsSingle();
        }
    }
}
