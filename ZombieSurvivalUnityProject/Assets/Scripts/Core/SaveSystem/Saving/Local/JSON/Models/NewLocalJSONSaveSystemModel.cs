using System;
using Core.SaveSystem.Enums;
using Core.SaveSystem.Saving.Common.Load;
using Core.SaveSystem.Saving.Common.Path;
using Core.SaveSystem.Saving.Interfaces;
using Newtonsoft.Json;
using Zenject;

namespace Core.SaveSystem.Saving.Local.JSON.Models
{
    public class NewLocalJSONSaveSystemModel : ISaveSystemModel
    {
        [Inject] private LocalStoragePathProvider PathProvider { get; }

        void ISaveSystemModel.Save(string entityId, string dataKey, SaveGroup saveGroup, object data, Action<bool> callback)
        {
            saveGroup.SaveEntity(entityId, dataKey, data);
        }

        void ISaveSystemModel.Load<T>(string entityId, string dataKey, SaveGroup saveGroup, Action<LoadResult<T>> callback)
        {
            var entityComponents = saveGroup.LoadEntity(entityId);
            
            if (entityComponents.TryGetValue(dataKey, out var entityComponentDataJson))
            {
                var entityComponentData = JsonConvert.DeserializeObject<T>(entityComponentDataJson.ToString());
                var loadResult = new LoadResult<T>(Result.LoadedSuccessfully, entityComponentData);

                callback?.Invoke(loadResult);
            }
            else
            {
                var loadResult = new LoadResult<T>(Result.LoadedWithErrors, new T());
                callback?.Invoke(loadResult);
            }
        }
    }
}