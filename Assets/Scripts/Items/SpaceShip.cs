using System;
using UnityEngine;

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
        [SerializeField] private float _thrust;

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

        [Header("UI")]
        [SerializeField] private UIArrow _UIArrow;

        [Header("Fire")]
        [SerializeField] private Turret[] _turrets;
        /// <summary>
        /// Maximum energy and ammos
        /// </summary>
        [SerializeField] private int _maxEnergy; 
        [SerializeField] private int _maxAmmo;

        /// <summary>
        /// Default values
        [SerializeField] private int _defaultEnergy;
        [SerializeField] private int _defaultAmmo;
        /// </summary>

        /// <summary>
        /// Regeneration of energy
        /// </summary>
        [SerializeField] private int _energyRegenPerSecond;

        private Rigidbody2D _rigid;
        /// <summary>
        /// Current energy and ammos
        /// </summary>
        private float _primaryEnergy;
        private int _secondaryAmmo;

        private float PrimaryEnergy { get => _primaryEnergy; 
                                      set 
                                      {
                                        _primaryEnergy = value;
                                        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
                                      }
                                    }

        private int SecondaryAmmo { get => _secondaryAmmo;
                                      set 
                                      {
                                        _secondaryAmmo = value;
                                        OnAmmoChanged?.Invoke(this, EventArgs.Empty);  
                                      }             
                                    }

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

        /// <summary>
        /// Energy's changed - event
        /// </summary>
        public event EventHandler<EventArgs> OnEnergyChanged;

        /// <summary>
        /// Ammo's changed - event
        public event EventHandler<EventArgs> OnAmmoChanged;
        /// </summary>

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

        private void FixedUpdate()
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

        private protected override void OnDeath() => ObjectDestroyer.Instance.DestroyGameObject(gameObject, 1.5f, DestroyEffect, _spriteRenderer, base.OnDeath);

        public void TuneHud(Transform target, Camera mainCamera) => _UIArrow?.TuneHud(target, mainCamera);

        public void AddEnergy(int energy) => PrimaryEnergy = Mathf.Clamp(PrimaryEnergy + energy, 0, _maxEnergy);        
        
        public void AddAmmo(int ammo) => SecondaryAmmo = Mathf.Clamp(SecondaryAmmo + ammo, 0, _maxAmmo);

        private void InitOffensive()
        {
            PrimaryEnergy = _defaultEnergy;
            SecondaryAmmo = _defaultAmmo;
        }

        private void UpdateEnergyRegen()
        {
            PrimaryEnergy += (float)_energyRegenPerSecond * Time.fixedDeltaTime;
            PrimaryEnergy = Mathf.Clamp(PrimaryEnergy, 0, _maxEnergy);
        }

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
    }
}
