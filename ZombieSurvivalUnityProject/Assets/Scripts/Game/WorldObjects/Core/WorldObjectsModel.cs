using System.Collections.Generic;
using System.Linq;
using Core.Lifetime.Instantiation;
using Core.SaveSystem.Entity;
using Core.SaveSystem.Models;
using Core.SaveSystem.Saving.Common.Load;
using Game.Items.Database;
using Game.Items.Properties.Implementations;
using Zenject;

namespace Game.WorldObjects.Core
{
    public class WorldObjectsModel : SaveableModel<WorldObjectsModel.Data>
    {
        public new class Data
        {
            public List<WorldObjectModel.BaseWorldObjectData> WorldObjectsData { get; set; } = new();
        }

        [Inject] private ItemsDataBase ItemsDataBase { get; }
        [Inject] private Instantiator Instantiator { get; }

        protected override string DataKey => "WorldObjectsModel.Data";

        private List<WorldObjectModel> _worldObjects = new();

        public WorldObjectsModel(SaveableEntity entity) : base(entity)
        {
        }

        protected override void HandleOnDataLoaded(LoadResult<Data> loadResult)
        {
            switch (loadResult.Result)
            {
                case Result.LoadedSuccessfully:
                    SpawnItems(loadResult.Data);
                    break;

                case Result.LoadedWithErrors:

                    break;
            }
        }

        protected override void HandleOnDataPreSaved()
        {
            base.Data.WorldObjectsData = _worldObjects
                .Select(x => x.WorldObjectData)
                .ToList();
        }

        public void Register(WorldObjectModel pickableItem)
        {
            _worldObjects.Add(pickableItem);
        }

        public void Unregister(WorldObjectModel pickableItem)
        {
            _worldObjects.Remove(pickableItem);
        }

        private void SpawnItems(Data data)
        {
            var worldObjectsData = data.WorldObjectsData;
            foreach (var worldObjectData in worldObjectsData)
            {
                var itemData = worldObjectData.ItemData;
                if (ItemsDataBase.TryGetItemData(itemData.Id, out var dbBaseItemData))
                {
                    if (dbBaseItemData.TryGetProperty<PickableItemProperty>(out var pickableItemProperty))
                    {
                        var worldObjectModel = Instantiator.InstantiatePrefabForComponent<WorldObjectModel>(
                            pickableItemProperty.WorldObjectPrefab);
                        worldObjectModel.Init(worldObjectData);
                    }
                }
            }
        }
    }
}
