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

        public bool Indestructible => _indestructible;
        public int HitPoints => _hitPoints;
        public int CurrentHitPoints { get; private set; }

        #endregion

        #region UnityEvents

        protected virtual void Start()
        {
            CurrentHitPoints = HitPoints;
        }

        #endregion

        #region Public API

        public void ApplyDamage(int damage)
        {
            if (_indestructible)
                return;

            CurrentHitPoints -= damage;

            if (CurrentHitPoints <= 0)
                OnDeath();
        }

        #endregion

        private protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }

    }
}
