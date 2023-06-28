using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode _mode;
        [SerializeField] private TurretProperties _turretProperties;

        private float _refireTimer;
        private SpaceShip _ship;

        public TurretMode Mode => _mode;
        public bool CanFire => _refireTimer <= 0;

        #region UnityEvents
        private void Start()
        {
            _ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (_refireTimer > 0) 
                _refireTimer -= Time.deltaTime;
        }
        #endregion

        // Public API
        public void Fire()
        {
            if (_turretProperties == null || _refireTimer > 0)
                return;

            Projectile projectile = Instantiate(_turretProperties.ProjectilePrefab, transform.position, Quaternion.identity)
                                                .GetComponent<Projectile>();

            projectile.transform.up = transform.up;

            _refireTimer = _turretProperties.RateOfFire;

            {
                // SFX
            }
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
