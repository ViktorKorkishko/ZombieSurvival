using Game.Inventory.DragAndDrop.Controllers;
using Game.Inventory.DragAndDrop.Models;
using UnityEngine;
using Zenject;

namespace Game.Inventory.DragAndDrop.Installers
{
    public class DragAndDropInstaller : MonoInstaller
    {
        [SerializeField] private Transform _dragObjectParent;
        
        public override void InstallBindings()
        {
            Container
                .Bind<DragAndDropModel>()
                .AsSingle();

            Container
                .BindInterfacesTo<DragAndDropController>()
                .AsSingle()
                .WithArguments(_dragObjectParent);
        }
    }
}
