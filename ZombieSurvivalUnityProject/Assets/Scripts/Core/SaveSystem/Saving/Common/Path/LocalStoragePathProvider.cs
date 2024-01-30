using UnityEngine;

namespace Core.SaveSystem.Saving.Common.Path
{
    public class LocalStoragePathProvider
    {
        public string Path => Application.persistentDataPath;
    }
}
