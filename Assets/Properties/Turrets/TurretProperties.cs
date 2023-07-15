using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode _mode;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _rateOfFire;
        [SerializeField] private int _energyUsage;
        [SerializeField] private int _ammoUsage;
        [SerializeField] private AudioClip _launchSFX;
                
        public TurretMode Mode => _mode;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public float RateOfFire => _rateOfFire;
        public int EnergyUsage => _energyUsage;
        public int AmmoUsage => _ammoUsage;
        public AudioClip LaunchSFX => _launchSFX;



    }
}
