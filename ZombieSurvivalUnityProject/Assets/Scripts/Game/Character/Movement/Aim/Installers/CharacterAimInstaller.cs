using Cinemachine;
using Game.Character.Movement.Aim.Controllers;
using Game.Character.Movement.Aim.Models;
using UnityEngine;
using Zenject;

namespace Game.Character.Movement.Aim.Installers
{
    public class CharacterAimInstaller : MonoInstaller
    {
        [SerializeField] private CharacterAimModel _characterAimModel;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public override void InstallBindings()
        {
            Container.Bind<CharacterAimModel>()
                .FromInstance(_characterAimModel)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterAimController>()
                .AsSingle();

            Container.BindInstance(_cinemachineVirtualCamera).AsSingle();
        }
    }
}
