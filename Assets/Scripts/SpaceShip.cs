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

        private Rigidbody2D _rigid;

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
        }        

        private void FixedUpdate()
        {
            UpdateRigidbody();
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

    }
}