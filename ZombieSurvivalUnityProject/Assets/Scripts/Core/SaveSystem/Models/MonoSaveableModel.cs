using System;
using Core.Exceptions;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Saving.Common.Load;
using Core.SaveSystem.Saving.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.SaveSystem.Models
{
    public abstract class MonoSaveableModel<TData> : MonoBehaviour, IInitializable, IDisposable 
        where TData : new()
    {
        [Inject] private ISaveSystemModel SaveSystemModel { get; }
        private SaveableEntity SaveableEntity { get; set; }
        
        protected abstract string DataKey { get; }
        protected TData Data => _data;
        
        private TData _data;
        
        [Inject]
        public void Construct(SaveableEntity entity)
        {
            SaveableEntity = entity;
        }
        
        void IInitializable.Initialize()
        {
            SaveSystemModel.Load<TData>(SaveableEntity.Id, DataKey, SaveableEntity.SaveGroup, loadResult =>
            {
                var data = loadResult.Data;
                switch (loadResult.Result)
                {
                    case Result.LoadedWithErrors:
                        _data = new TData();
                        break;
                    
                    case Result.SaveFileNotFound:
                        _data = new TData();
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
        
        void IDisposable.Dispose()
        {
            HandleOnDataPreSaved();
            SaveSystemModel.Save(SaveableEntity.Id, DataKey, SaveableEntity.SaveGroup, _data);
        }
        
        protected virtual void HandleOnDataLoaded(LoadResult<TData> loadResult) { }
        protected virtual void HandleOnDataPreSaved() { }
    }
}
