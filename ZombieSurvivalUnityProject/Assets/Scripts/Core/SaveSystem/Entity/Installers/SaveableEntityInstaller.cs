using UnityEngine;
using Zenject;

namespace Core.SaveSystem.Entity.Installers
{
    public class SaveableEntityInstaller : MonoInstaller
    {
        [SerializeField] private SaveableEntity _saveableEntity;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_saveableEntity).AsSingle();
        }
    }
}
