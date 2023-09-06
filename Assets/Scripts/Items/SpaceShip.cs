using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [Header("Space ship")]

        /// <summary>
        /// Mass of the ship (for rigid)
        /// </summary>
        [SerializeField] private float _mass;

        /// <summary>
        /// Pushing forward force 
        /// </summary>
        [SerializeField] protected float _thrust;

        /// <summary>
        /// Rotation force 
        /// </summary>
        [SerializeField] private float _mobility;

        /// <summary>
        /// Max linear speed 
        /// </summary>
        [SerializeField] private float _maxLinearVelocity;

        /// <summary>
        /// Max rotation speed (degrees per second) 
        /// </summary>
        [SerializeField] private float _maxAngularVelocity;

        [Header("Fire")]
        [SerializeField] private Turret[] _turrets;
        /// <summary>
        /// Maximum energy and ammos
        /// </summary>
        [SerializeField] protected int _maxEnergy;
        [SerializeField] protected int _maxAmmo;

        /// <summary>
        /// Default values
        [SerializeField] protected int _defaultEnergy;
        [SerializeField] protected int _defaultAmmo;
        /// </summary>

        /// <summary>
        /// Regeneration of energy
        /// </summary>
        [SerializeField] protected int _energyRegenPerSecond;

        /// <summary>
        /// Visual
        /// </summary>
        [SerializeField] private Sprite _previewImage;

        private Rigidbody2D _rigid;
        /// <summary>
        /// Current energy and ammos
        /// </summary>
        protected float _primaryEnergy;
        protected int _secondaryAmmo;

        
        #region Public API

        /// <summary>
        /// Linear thrust control: -1.0 .. 1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotation thrust control: -1.0 .. 1.0
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// Energy counter
        /// </summary>
        public float CurrentEnergy { get => _primaryEnergy; }

        /// <summary>
        /// Ammo counter
        /// </summary>  
        public int CurrentAmmo { get => _secondaryAmmo; }

        public virtual float PrimaryEnergy
        {
            get => _primaryEnergy;
            set
            {
                _primaryEnergy = value;                
            }
        }

        public virtual int SecondaryAmmo
        {
            get => _secondaryAmmo;
            set
            {
                _secondaryAmmo = value;                
            }
        }

        public float MaxLinearVelocity => _maxLinearVelocity;
        public float MaxAngularVelocity => _maxAngularVelocity;
        public Sprite PreviewImage => _previewImage;

        #endregion

        #region UnityEvents

        protected override void Start()
        {
            base.Start();

            _rigid = GetComponent<Rigidbody2D>();
            _rigid.mass = _mass;
            _rigid.inertia = 1;

            InitOffensive();
        }

        protected virtual void FixedUpdate()
        {
            UpdateRigidbody();
            UpdateEnergyRegen();
        } 

        #endregion

        /// <summary>
        /// Add force for moving of the ship
        /// </summary>
        private void UpdateRigidbody()
        {
            _rigid.AddForce(ThrustControl * _thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigid.AddForce(-_rigid.velocity * (_thrust / _maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            _rigid.AddTorque(TorqueControl * _mobility * Time.deltaTime, ForceMode2D.Force);
            _rigid.AddTorque(-_rigid.angularVelocity * (_mobility / _maxAngularVelocity) * Time.deltaTime, ForceMode2D.Force);
        }

        public void Fire(TurretMode mode)
        {
            foreach (Turret turret in _turrets)
            {
                if (turret.Mode == mode)
                    turret.Fire();
            }
        }

        public void Fire(TurretMode mode, Vector3 dest)
        {
            foreach (Turret turret in _turrets)
            {
                if (turret.Mode == mode)
                {
                    if (turret.Fire(out Projectile projectile))
                    {
                        float targetAngle = Vector2.SignedAngle(transform.up, dest);
                        targetAngle += projectile.transform.eulerAngles.z;
                        projectile.transform.eulerAngles = new Vector3(0, 0, targetAngle);
                    }                                        
                        
                }
                    
            }
        }

        private void InitOffensive()
        {
            PrimaryEnergy = _defaultEnergy;
            SecondaryAmmo = _defaultAmmo;            
        }

        protected override void OnDeath() => ObjectDestroyer.Instance.DestroyGameObject(gameObject, 1.5f, DestroyEffect, _spriteRenderer, base.OnDeath);

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (SecondaryAmmo >= count)
            {
                SecondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (PrimaryEnergy >= count)
            {
                PrimaryEnergy -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties turretProperties)
        {
            foreach (Turret turret in _turrets)
                turret.AssignLoadOut(turretProperties);            
        }

        private void UpdateEnergyRegen()
        {
            PrimaryEnergy += (float)_energyRegenPerSecond * Time.fixedDeltaTime;
            PrimaryEnergy = Mathf.Clamp(PrimaryEnergy, 0, _maxEnergy);
        }

    }
}
