using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// This is the object which can be destructible
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        [SerializeField] internal bool _indestructible;
        [SerializeField] private int _hitPoints;
        [SerializeField] private ParticleSystem _destroyEffect;
        [SerializeField] internal SpriteRenderer _spriteRenderer;

        private static HashSet<Destructible> _allDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => _allDestructibles;

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

        protected virtual void OnDeath() => OnDestruct?.Invoke(this, EventArgs.Empty);

        protected virtual void OnEnable()
        {
            if (_allDestructibles == null)
                _allDestructibles = new HashSet<Destructible>();
            
            _allDestructibles.Add(this);            
        }

        private void OnDestroy() => _allDestructibles.Remove(this);

    }
}
