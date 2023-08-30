using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode _mode;
        [SerializeField] private TurretProperties _turretProperties;
        [SerializeField] private SpaceShip _ship;

        private float _refireTimer;
        
        public TurretMode Mode => _mode;
        public bool CanFire => _refireTimer <= 0;

        #region UnityEvents
        
        private void Update()
        {
            if (_refireTimer > 0) 
                _refireTimer -= Time.deltaTime;
        }
        #endregion

        // Public API

        public void Fire() => Fire(out Projectile projectile);

        public bool Fire(out Projectile projectile)
        {
            projectile = null;

            if (_turretProperties == null || _refireTimer > 0)
                return false;

            if (_ship.DrawEnergy(_turretProperties.EnergyUsage) == false
                ||
                _ship.DrawAmmo(_turretProperties.AmmoUsage) == false)
                return false;

            projectile = Instantiate(_turretProperties.ProjectilePrefab, transform.position, Quaternion.identity)
                                                .GetComponent<Projectile>();

            projectile.transform.up = transform.up;
            projectile.SetParrentShooter(_ship);

            _refireTimer = _turretProperties.RateOfFire;

            {
                // SFX
            }

            return true;
        }

        public void AssignLoadOut(TurretProperties properties)
        {
            if (_mode != properties.Mode)
                return;

            _refireTimer = 0;
            _turretProperties = properties;
        }
    }
}
