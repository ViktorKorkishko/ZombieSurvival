using System;
using Core.Exceptions;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Saving.Common.Load;
using Core.SaveSystem.Saving.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.SaveSystem.Models
{
    public abstract class MonoSaveableModel<T> : MonoBehaviour, IInitializable, IDisposable 
        where T : new()
    {
        [Inject] private ISaveSystemModel SaveSystemModel { get; }
        [Inject] private SaveableEntity SaveableEntity { get; }

        protected abstract string DataKey { get; }
        protected T Data => _data;

        private string Id => SaveableEntity.Id;

        private T _data;

        public virtual void Initialize()
        {
            SaveSystemModel.Load<T>(Id, DataKey, SaveableEntity.SaveGroup, loadResult =>
            {
                var data = loadResult.Data;
                switch (loadResult.Result)
                {
                    case Result.LoadedWithErrors:
                        _data = new T();
                        break;
                    
                    case Result.SaveFileNotFound:
                        _data = new T();
                        break;
                    
                    case Result.LoadedSuccessfully:
                        _data = data;
                        break;

                    default:
                        Debug.LogError(new EnumNotSupportedException<Result>(loadResult.Result));
                        break;
                }

                HandleOnDataLoaded(loadResult);
            });
        }

        public virtual void Dispose()
        {
            HandleOnDataPreSaved();
            
            SaveSystemModel.Save(Id, DataKey, SaveableEntity.SaveGroup, _data);
        }

        protected virtual void HandleOnDataLoaded(LoadResult<T> loadResult)
        {
        }

        protected virtual void HandleOnDataPreSaved()
        {
        }
    }
}
