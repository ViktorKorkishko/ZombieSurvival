using Core.Lifetime.Facade;
using Game.InteractableObjects.Implementations.PickableItem.Models;
using Game.Items.Data;

namespace Game.Weapons.WorldObject.Facade
{
    public class WeaponWorldObjectFacade : FacadeBase
    {
        private PickableItemModel PickableItemModel => DiContainer.Resolve<PickableItemModel>();

        public override void Init(ItemData data)
        {
            PickableItemModel.Initialize(data);
        }
    }
}
