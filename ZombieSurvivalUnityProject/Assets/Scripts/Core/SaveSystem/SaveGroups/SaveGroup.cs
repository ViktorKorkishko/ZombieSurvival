using System.Collections.Generic;
using System.IO;
using Core.SaveSystem.Saving.Common.Path;
using Newtonsoft.Json;
using Zenject;

namespace Core.SaveSystem.Enums
{
    public class SaveGroup : IInitializable
    {
        [Inject] private LocalStoragePathProvider PathProvider { get; }
        
        public SaveGroupId SaveGroupId { get; }

        private Dictionary<string, Dictionary<string, object>> _objectToDataDictionary = new ();
        
        private const string k_jsonFileExtension = ".json";

        public SaveGroup(SaveGroupId saveGroupId)
        {
            SaveGroupId = saveGroupId;
        }

        void IInitializable.Initialize()
        {
            InitData();
        }

        private void InitData()
        {
            string saveGroupFilePath = BuildPath();

            bool fileExists = File.Exists(saveGroupFilePath);
            if (!fileExists)
            {
                var createFileStream = File.Create(saveGroupFilePath);
                createFileStream.Dispose();
            }

            using var readFileStream = new StreamReader(saveGroupFilePath);
            var saveGroupJson = readFileStream.ReadToEnd();

            bool noSaveFiles = string.IsNullOrEmpty(saveGroupJson);
            if (noSaveFiles)
            {
                _objectToDataDictionary = new Dictionary<string, Dictionary<string, object>>();
            }
            else
            {
                _objectToDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(saveGroupJson);
            }
        }

        public Dictionary<string, object> LoadEntity(string entityId)
        {
            if (_objectToDataDictionary.TryGetValue(entityId, out var entityComponentsData))
            {
                return entityComponentsData;
            }

            return new Dictionary<string, object>();
        }

        public void SaveEntity<T>(string entityId, string dataKey, T data)
        {
            if (_objectToDataDictionary.TryGetValue(entityId, out var entityComponentsData))
            {
                entityComponentsData[dataKey] = data;
            }
            else
            {
                var entityComponent = new Dictionary<string, object>
                {
                    { dataKey, data }
                };
                _objectToDataDictionary.Add(entityId, entityComponent);
            }
            
            SaveDictionary();
            
            // callback?.Invoke(true);
        }
            
        private void SaveDictionary()
        {
            string path = BuildPath();
            
            string dictionaryJson = JsonConvert.SerializeObject(_objectToDataDictionary, Formatting.Indented);
            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(dictionaryJson);
            }
        }

        private string BuildPath()
        {
            var path = Path.Combine(PathProvider.Path, SaveGroupId + k_jsonFileExtension);
            return path;
        }
    }
}
