using UnityEngine;

namespace Game.Weapons.Common.Config
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Game/Weapons/Shooting/Config")]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Damage")]
        [SerializeField] private int _damage;

        [Header("Rate of fire")]
        [Tooltip("Bullets per minute")]
        [SerializeField] private int _fireRate;
        [SerializeField] private int _bulletsPerTap;

        [Header("Spread")]
        [SerializeField] private float _spread;
        [SerializeField] private float _range;

        [Header("Reload")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _magazineSize;

        public int Damage => _damage;

        public int FireRate => _fireRate;
        public int BulletsPerTap => _bulletsPerTap;

        public float Spread => _spread;
        public float Range => _range;
        
        public float ReloadTime => _reloadTime;
        public int MagazineSize => _magazineSize;
    }
}
