using Game.Inventory.Core.Controllers;
using Game.Inventory.Core.Models;
using UnityEngine;
using Zenject;

namespace Game.Inventory.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private int _cellsCount;
        
        public override void InstallBindings()
        {
            Container
                .Bind<InventoryModel>()
                .AsSingle()
                .WithArguments(_cellsCount);
            
            Container
                .BindInterfacesAndSelfTo<InventoryController>()
                .AsSingle();
        }
    }
}
