using UnityEngine;

namespace Game.Weapons.Common
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Game/Weapons/Shooting/Config")]
    public class WeaponConfig : ScriptableObject
    {
        [field:SerializeField] public int Damage { get; private set; }
        [field:SerializeField] public int FireRate { get; private set; }
        [field:SerializeField] public float Range { get; private set; }
        [field:SerializeField] public float  ReloadTime { get; private set; }
        [field:SerializeField] public int MagazineSize { get; private set; }
    }
}
