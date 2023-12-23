using Game.Character.Interaction.Controllers;
using Game.Character.Interaction.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Interaction.Installers
{
    public class CharacterObjectInteractionInstaller : MonoInstaller
    {
        [SerializeField] private CharacterObjectInteractionModel _model;

        public override void InstallBindings()
        {
            Container.BindInstance(_model).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterObjectInteractionController>().AsSingle();
        }
    }
}
