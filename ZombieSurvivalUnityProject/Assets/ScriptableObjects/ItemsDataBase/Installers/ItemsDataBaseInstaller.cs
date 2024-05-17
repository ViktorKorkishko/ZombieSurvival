using UnityEngine;
using Zenject;

namespace ScriptableObjects.ItemsDataBase.Installers
{
    [CreateAssetMenu(fileName = "ItemsDatabaseInstaller", menuName = "Installers/ItemsDatabaseInstaller")]
    public class ItemsDataBaseInstaller : ScriptableObjectInstaller<ItemsDataBaseInstaller>
    {
        [SerializeField] private Game.Items.Database.ItemsDataBase _itemsDataBase;
    
        public override void InstallBindings()
        {
            Container
                .Bind<Game.Items.Database.ItemsDataBase>()
                .FromInstance(_itemsDataBase)
                .AsSingle();
        }
    }
}
