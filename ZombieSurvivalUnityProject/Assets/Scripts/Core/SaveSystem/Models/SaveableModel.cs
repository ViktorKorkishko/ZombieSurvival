using Core.Exceptions;
using Core.Lifetime.Initialization;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Saving.Common.Load;
using Core.SaveSystem.Saving.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.SaveSystem.Models
{
    public abstract class SaveableModel<TData> : SelfInitializableModel 
        where TData : new()
    {
        [Inject] private ISaveSystemModel SaveSystemModel { get; }
        private SaveableEntity SaveableEntity { get; }
        
        protected abstract string DataKey { get; }
        protected TData Data => _data;

        private TData _data;

        public SaveableModel(SaveableEntity entity)
        {
            SaveableEntity = entity;
        }
        
        public override void Initialize()
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

            Initialized = true;
        }

        public override void Dispose()
        {
            HandleOnDataPreSaved();
            SaveSystemModel.Save(SaveableEntity.Id, DataKey, SaveableEntity.SaveGroup, _data,_ => Initialized = false);
        }
        
        protected virtual void HandleOnDataLoaded(LoadResult<TData> loadResult) { }
        protected virtual void HandleOnDataPreSaved() { }
    }
}
