using Game.Character.Movement.Locomotion.Controllers;
using Game.Character.Movement.Locomotion.Models;
using Zenject;

namespace Game.Character.Movement.Locomotion.Installers
{
    public class CharacterMovementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterMovementModel>().AsSingle();
            Container.Bind<CharacterMovementController>().AsSingle();
        }
    }
}
