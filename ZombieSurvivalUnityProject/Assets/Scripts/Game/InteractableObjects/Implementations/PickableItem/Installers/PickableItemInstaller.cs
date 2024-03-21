using Game.InteractableObjects.Common.Installers;
using Game.InteractableObjects.Implementations.PickableItem.Controllers;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using UnityEngine;

namespace Game.InteractableObjects.Implementations.PickableItem.Installers
{
    public class PickableItemInstaller : InteractableObjectInstallerBase<PickableItemController>
    {
        [Header("Secondary implementation")]
        [SerializeField] private PickableItemModel _pickableItemModel;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.Bind<PickableItemModel>()
                .FromInstance(_pickableItemModel)
                .AsSingle();
        }
    }
}
