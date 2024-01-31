using System;
using System.IO;
using Core.SaveSystem.Saving.Common.Load;
using Core.SaveSystem.Saving.Common.Path;
using Core.SaveSystem.Saving.Interfaces;
using Newtonsoft.Json;
using Zenject;

namespace Core.SaveSystem.Saving.Local.JSON.Models
{
    public class LocalJSONSaveSystemModel : ISaveSystemModel
    {
        [Inject] private LocalStoragePathProvider PathProvider { get; }

        private const string k_jsonFileExtension = ".json";

        void ISaveSystemModel.Save(string key, object data, Action<bool> callback)
        {
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(data);

            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }

            callback?.Invoke(true);
        }

        void ISaveSystemModel.Load<T>(string key, Action<LoadResult<T>> callback)
        {
            Result result;
            
            string path = BuildPath(key);
            
            bool fileExists = File.Exists(path);
            if (fileExists)
            {
                result = Result.LoadedSuccessfully;
            }
            else
            {
                result = Result.SaveFileNotFound;
                
                var createFileStream = File.Create(path);
                createFileStream.Dispose();
            }
            
            using (var readFileStream = new StreamReader(path))
            {
                var json = readFileStream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                
                var loadResult = new LoadResult<T>(result, data);
                
                callback.Invoke(loadResult);
            }
        }

        private string BuildPath(string key)
        {
            var path = Path.Combine(PathProvider.Path, key + k_jsonFileExtension);
            return path;
        }
    }
}