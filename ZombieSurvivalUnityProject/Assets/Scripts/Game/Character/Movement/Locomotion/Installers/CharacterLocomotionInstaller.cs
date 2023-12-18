using Game.Character.Movement.Locomotion.Controllers;
using Game.Character.Movement.Locomotion.Models;
using Zenject;

namespace Game.Character.Movement.Locomotion.Installers
{
    public class CharacterLocomotionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterLocomotionModel>().AsSingle();
            Container.Bind<CharacterLocomotionController>().AsSingle();
        }
    }
}
