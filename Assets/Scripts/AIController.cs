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
        [SerializeField] private AIPointPatrol _patrolPoint;

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
        private const float MAX_ANGLE = 45.0F;
        private Timer _randomizeDirectionTimer;

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
            _spaceShip.TorqueControl = ComputeAliginTorqueNormalize(_movePosition, _spaceShip.transform) * _navigationAngular;

        }

        private static float ComputeAliginTorqueNormalize(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;


            return -angle;
        }

        private void ActionFindNewMovePosition()
        {
            if (_behaviour != AIBehaviour.Patrol || _patrolPoint == null)
                return;

            if (_selectedTarget != null)
            {
                _movePosition = _selectedTarget.transform.position;
                return;
            }              

           // Debug.Log(_randomizeDirectionTimer.CurrentTime);
            
            bool isInsidePatrolZone = (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.Radius * _patrolPoint.Radius;

            if (isInsidePatrolZone)
            {
                if (_randomizeDirectionTimer.IsFinished == true)
                {
                    _movePosition = UnityEngine.Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
                    _randomizeDirectionTimer.Start(_randomSelectMovePointType);
                }
            }
            else
                _movePosition = _patrolPoint.transform.position;                                                
        }

        private void SetPatrolBehaviour(AIPointPatrol pointPatrol)
        {
            _behaviour = AIBehaviour.Patrol;
            _patrolPoint = pointPatrol;
        }

        #region Timers

        private void InitTimers()
        {
            InitTimer(ref _randomizeDirectionTimer, _randomSelectMovePointType);            
        }

        private void InitTimer(ref Timer timer, float interval) => timer = new Timer(interval);

        private void UpdateTimers()
        {
            UpdateTimer(_randomizeDirectionTimer);            
        }

        private void UpdateTimer(Timer timer) => timer.SubstractTime(Time.deltaTime);

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, _movePosition);
            Debug.Log(_movePosition);
        }


    }
}
