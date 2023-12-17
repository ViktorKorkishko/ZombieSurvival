using UnityEngine;

namespace Game.Weapons.Shooting.Config
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Game/Weapons/Shooting/Config")]
    public class WeaponShootingConfig : ScriptableObject
    {
        [Header("Damage")]
        [SerializeField] private int _damage;

        [Header("Rate of fire")]
        [SerializeField] private float _timeBetweenShooting;
        [SerializeField] private float _timeBetweenShots;
        [SerializeField] private int _bulletsPerTap;

        [Header("Spread")]
        [SerializeField] private float _spread;
        [SerializeField] private float _range;

        [Header("Reload")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _magazineSize;

        public int Damage => _damage;

        public float TimeBetweenShooting => _timeBetweenShooting;
        public float TimeBetweenShots => _timeBetweenShots;
        public int BulletsPerTap => _bulletsPerTap;

        public float Spread => _spread;
        public float Range => _range;
        
        public float ReloadTime => _reloadTime;
        public int MagazineSize => _magazineSize;
    }
}
