using Zenject;

namespace Game.Character.Weapons.Equip.Models
{
    public class EquippedWeapon
    {
        public DiContainer WeaponContainer { get; }

        public EquippedWeapon(DiContainer container)
        {
            WeaponContainer = container;
        }
        
        public T GetComponent<T>()
        {
            return (T)WeaponContainer.Resolve(typeof(T));
        }
        
        public T GetComponentWithId<T>(object identifier)
        {
            var component = WeaponContainer.ResolveId<T>(identifier);
            return component;
        }
    }
}
