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
        [SerializeField] private ParticleSystem _destroyEffect;
        [SerializeField] internal SpriteRenderer _spriteRenderer;
        
        public ParticleSystem DestroyEffect 
        {
            get { return _destroyEffect; }
            set { if (value != null)
                    _destroyEffect = value; } 
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
                OnDeath();                                            
        }

        #endregion

        private protected virtual void OnDeath() => OnDestruct?.Invoke(this, EventArgs.Empty);

    }
}
