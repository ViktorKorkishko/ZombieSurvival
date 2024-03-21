using Game.Character.Interaction.Controllers;
using Game.Character.Interaction.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Interaction.Installers
{
    public class CharacterObjectInteractionInstaller : MonoInstaller
    {
        [SerializeField] private CharacterObjectInteractionModel _characterObjectInteractionModel;

        public override void InstallBindings()
        {
            Container
                .Bind<CharacterObjectInteractionModel>()
                .FromInstance(_characterObjectInteractionModel)
                .AsSingle();
            
            Container
                .BindInterfacesTo<CharacterObjectInteractionController>()
                .AsSingle();
        }
    }
}
