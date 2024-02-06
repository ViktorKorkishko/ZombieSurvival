using System;
using Core.SaveSystem.Enums;
using Core.SaveSystem.Saving.Common.Load;

namespace Core.SaveSystem.Saving.Interfaces
{
    public interface ISaveSystemModel
    {
        void Save(string entityId, string dataKey, SaveGroup saveGroup, object data, Action<bool> callback = null);
        void Load<T>(string entityId, string dataKey, SaveGroup saveGroup, Action<LoadResult<T>> callback = null) where T : new();
    }
}
