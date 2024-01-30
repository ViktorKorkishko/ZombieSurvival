using System;

namespace Core.SaveSystem.Saving.Interfaces
{
    public interface ISaveSystemModel
    {
        void Save(string key, object data, Action<bool> callback = null);
        void Load<T>(string key, Action<T> callback = null);
    }
}
