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
        /// Regeneration of energy
        /// </summary>
        [SerializeField] private int _energyRegenPerSecond;

        private Rigidbody2D _rigid;
        /// <summary>
        /// Current energy and ammos
        /// </summary>
        private float _primaryEnergy;
        private int _secondaryAmmo;

        #region Public API

        /// <summary>
        /// Linear thrust control: -1.0 .. 1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotation thrust control: -1.0 .. 1.0
        /// </summary>
        public float TorqueControl { get; set; }

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

        public void AddEnergy(int energy) => _primaryEnergy = Mathf.Clamp(_primaryEnergy + energy, 0, _maxEnergy);        
        
        public void AddAmmo(int ammo) => _secondaryAmmo = Mathf.Clamp(_secondaryAmmo + ammo, 0, _maxAmmo);

        private void InitOffensive()
        {
            _primaryEnergy = _maxEnergy;
            _secondaryAmmo = _maxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            _primaryEnergy += (float)_energyRegenPerSecond * Time.fixedDeltaTime;
            _primaryEnergy = Mathf.Clamp(_primaryEnergy, 0, _maxEnergy);
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (_secondaryAmmo >= count)
            {
                _secondaryAmmo -= count;
                return true;
            }                

            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (_primaryEnergy >= count)
            {
                _primaryEnergy -= count;
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
