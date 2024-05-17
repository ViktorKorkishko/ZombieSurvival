using Game.ItemsDB.Item.Enums;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    public class ItemIdInstaller : MonoInstaller
    {
        [SerializeField] private ItemId _itemId;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemId>()
                .FromInstance(_itemId)
                .AsSingle();
        }
    }
}
