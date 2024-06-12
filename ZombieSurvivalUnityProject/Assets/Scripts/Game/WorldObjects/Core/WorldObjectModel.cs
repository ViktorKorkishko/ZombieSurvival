using System;
using Core.Installers;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using Game.Items.Data;
using UnityEngine;
using Zenject;

namespace Game.WorldObjects.Core
{
    public class WorldObjectModel : MonoBehaviour
    {
        [Serializable]
        public class BaseWorldObjectData
        {
            public Vector3 Position { get; set; }
            public Quaternion Rotation { get; set; }
            public ItemData ItemData { get; set; }
        }
        
        [Inject] private WorldObjectsModel WorldObjectsModel { get; }

        public BaseWorldObjectData WorldObjectData { get; private set; }

        private PickableItemModel PickableItemModel => LocalDiContainer.Resolve<PickableItemModel>();
        private Transform RootTransform => LocalDiContainer.ResolveId<Transform>(BindingIdentifiers.ViewRoot);

        private DiContainer LocalDiContainer { get; set; }

        public void InitDiContainer(DiContainer diContainer)
        {
            LocalDiContainer = diContainer;
        }

        public void Init(BaseWorldObjectData baseWorldObjectData)
        {
            WorldObjectData = baseWorldObjectData;
            RootTransform.position = baseWorldObjectData.Position;
            RootTransform.rotation = baseWorldObjectData.Rotation;
            
            PickableItemModel.Initialize(baseWorldObjectData.ItemData);
            PickableItemModel.OnPickedUp += HandleOnPickedUp;
        }

        private void HandleOnPickedUp()
        {
            PickableItemModel.OnPickedUp -= HandleOnPickedUp;
            WorldObjectsModel.Unregister(this);
        }
    }
}
