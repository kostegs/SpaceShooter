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
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected int _teamID;
        [SerializeField] ParticleSystem _damagePS;

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
        public int TeamID => _teamID;

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

            if (_damagePS != null)
            {
                ParticleSystem particleSystem = Instantiate(_damagePS);
                particleSystem.transform.position = transform.position;
                particleSystem.Play();                
                Destroy(particleSystem.gameObject, particleSystem.main.duration);
            }            

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

        #region Score

        [SerializeField] private int _scoreValue;

        public int ScoreValue => _scoreValue;

        #endregion

    }
}
