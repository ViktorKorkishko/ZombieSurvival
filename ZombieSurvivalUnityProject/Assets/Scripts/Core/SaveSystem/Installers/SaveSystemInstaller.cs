using Core.SaveSystem.SaveGroups;
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
            Container
                .Bind<ISaveSystemModel>()
                .To<LocalJSONSaveSystemModel>()
                .AsSingle();
            
            Container
                .Bind<LocalStoragePathProvider>()
                .AsSingle();

            #region SaveGroups

            BindSaveGroup(SaveGroupId.Character);
            BindSaveGroup(SaveGroupId.GameWorld);
            BindSaveGroup(SaveGroupId.Project);
            BindSaveGroup(SaveGroupId.Inventory);
            
            #endregion
        }

        private void BindSaveGroup(SaveGroupId saveGroupId)
        {
            Container.BindInterfacesAndSelfTo<SaveGroup>()
                .AsCached()
                .WithArguments(saveGroupId);
        }
    }
}
