using System;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// This is the object which can be destructible
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] private bool _indestructible;
        [SerializeField] private int _hitPoints;

        private ParticleSystem _explosionParticleSystem;
        public ParticleSystem ExplosionParticleSystem 
        {
            get { return _explosionParticleSystem; }
            set { if (value != null)
                    _explosionParticleSystem = value; } 
        }                

        public bool Indestructible => _indestructible;
        public int HitPoints => _hitPoints;
        public int CurrentHitPoints { get; private set; }

        public event EventHandler<EventArgs> OnDestruct;
        public event EventHandler<EventArgs> OnHit;

        #endregion

        #region UnityEvents

        protected virtual void Start() => CurrentHitPoints = HitPoints;

        #endregion

        #region Public API

        public void ApplyDamage(int damage)
        {
            if (_indestructible)
                return;

            CurrentHitPoints -= damage;
            OnHit?.Invoke(this, EventArgs.Empty);

            if (CurrentHitPoints <= 0)
            {
                OnDeath();
                Destroy(gameObject);
            }                
        }

        #endregion

        private protected virtual void OnDeath() => OnDestruct?.Invoke(this, EventArgs.Empty);

    }
}
