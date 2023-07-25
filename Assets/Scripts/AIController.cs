using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour _behaviour;

        [Range(0f, 1f)]
        [SerializeField] private float _navigationLinear;

        [Range(0f, 1f)]
        [SerializeField] private float _navigationAngular;

        [SerializeField] private float _randomSelectMovePointType;
        [SerializeField] private float _findNewTargetTime;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _evadeRayLength;

        private SpaceShip _spaceShip;
        private Vector3 _movePosition;
        private Destructible _selectedTarget;
        private Timer _testTimer;

        private void Start()
        {
            _spaceShip = GetComponent<SpaceShip>();
            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
        }

        private void UpdateAI()
        {
            if(_behaviour == AIBehaviour.Patrol)
                UpdateBehaviourPatrol();            
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
        }

        private void ActionFire()
        {
            
        }

        private void ActionFindNewAttackTarget()
        {
            
        }

        private void ActionControlShip()
        {
            _spaceShip.ThrustControl = _navigationLinear;
            //ComputeAliginTorqueNormalize()

        }

        private static float ComputeAliginTorqueNormalize(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -45, 45) / 45;

            return -angle;
        }

        private void ActionFindNewMovePosition()
        {
            
        }

        #region Timers

        private void InitTimers()
        {

        }

        private void UpdateTimers()
        {

        }

        #endregion


    }
}
