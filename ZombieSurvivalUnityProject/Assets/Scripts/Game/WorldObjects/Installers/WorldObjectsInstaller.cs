using Core.SaveSystem.Entity;
using Game.WorldObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.WorldObjects.Installers
{
    public class WorldObjectsInstaller : MonoInstaller
    {
        [SerializeField] private SaveableEntity _saveableEntity;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<WorldObjectsModel>()
                .AsSingle()
                .WithArguments(_saveableEntity);
        }
    }
}
