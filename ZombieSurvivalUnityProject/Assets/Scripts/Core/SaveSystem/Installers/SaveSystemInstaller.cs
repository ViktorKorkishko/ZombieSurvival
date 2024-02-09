using Core.Installers;
using Core.SaveSystem.Enums;
using Core.SaveSystem.Saving.Common.Path;
using Core.SaveSystem.Saving.Interfaces;
using Core.SaveSystem.Saving.Local.JSON.Models;
using Zenject;

namespace Core.SaveSystem.Installers
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<ISaveSystemModel>().To<LocalJSONSaveSystemModel>().AsSingle();
            Container.Bind<ISaveSystemModel>().To<NewLocalJSONSaveSystemModel>().AsSingle();
            
            Container.Bind<LocalStoragePathProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SaveGroup>()
                .AsCached()
                .WithArguments(SaveGroupId.Character);
            
            Container.BindInterfacesAndSelfTo<SaveGroup>()
                .AsCached()
                .WithArguments(SaveGroupId.GameWorld);
            
            Container.BindInterfacesAndSelfTo<SaveGroup>()
                .AsCached()
                .WithArguments(SaveGroupId.Project);
        }
    }
}
