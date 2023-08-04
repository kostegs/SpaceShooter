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
        private const float MAX_ANGLE = 90.0F;
        
        private Timer _randomizeDirectionTimer;
        private Timer _fireTimer;
        private Timer _findNewTargetTimer;

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
            ActionEvadeCollision();
        }

        private void ActionFire()
        {
            if(_selectedTarget != null && _fireTimer.IsFinished == true)
            {
                _spaceShip.Fire(TurretMode.Primary);
                _fireTimer.Restart();
            }            
        }

        private void ActionFindNewAttackTarget()
        {
            if(_findNewTargetTimer.IsFinished == true)
            {
                _selectedTarget = FindNearestDestructibleTarget();
                _findNewTargetTimer.Restart();
            }            
        }

        private Destructible FindNearestDestructibleTarget()
        {
            //float maxDist = float.MaxValue;
            float maxDist = 10 * 10;

            Destructible potentionalTarget = null;

            foreach(var dest in Destructible.AllDestructibles)
            {
                // Don't check the current object or common team.
                /*if (dest.TryGetComponent<SpaceShip>(out SpaceShip destSpaceShip))
                {
                    if (destSpaceShip == _spaceShip || destSpaceShip.TeamID == _spaceShip.TeamID)
                        continue;
                }*/

                if (dest.TryGetComponent<PlayerSpaceShip>(out PlayerSpaceShip destSpaceShip))
                {
                    return dest;
                }
                    
                
                float dist = (dest.transform.position - _spaceShip.transform.position).sqrMagnitude;

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentionalTarget = dest;
                }                    
            }

            return potentionalTarget;

        }

        private void ActionControlShip()
        {
            var speed = _navigationLinear;

            if (_selectedTarget != null)
            {
                var dest = (transform.position - _selectedTarget.transform.position).sqrMagnitude;

                if (dest <= 10 * 10)                
                    speed = 0;                
            }                    

            _spaceShip.ThrustControl = speed;
            _spaceShip.TorqueControl = ComputeAliginTorqueNormalize(_movePosition, _spaceShip.transform) * _navigationAngular;

        }

        private float ComputeAliginTorqueNormalize(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            if (_selectedTarget != null)
                angle *= 10;

            

            return -angle;
        }

        private Vector3 CalculateLeadPosition(Destructible selectedTarget)
        {
            Vector2 toTarget = transform.position - _selectedTarget.transform.position;
            float xTime = toTarget.x / 10;
            float yTime = toTarget.y / 10;            

            float maxTime = Mathf.Max(Mathf.Abs(xTime), Mathf.Abs(yTime));

            /*
            Debug.Log($"Max time {maxTime}");

            Debug.Log($"Velocity: {selectedTarget.GetComponent<Rigidbody2D>().velocity}");
            Debug.Log($"selected target.position {selectedTarget.transform.position}");*/

            Vector2 goalPosition = (Vector2)selectedTarget.transform.position + (selectedTarget.GetComponent<Rigidbody2D>().velocity * maxTime);

            //Debug.Log($"goalPosition {goalPosition}");

            Debug.DrawLine(Vector2.zero, goalPosition, Color.yellow);

            return (Vector3)goalPosition;
        }

        private void ActionFindNewMovePosition()
        {
            if (_behaviour != AIBehaviour.Patrol || _patrolPoint == null)
                return;

            if (_selectedTarget != null)
            {
                //_movePosition = _selectedTarget.transform.position;
                _movePosition = CalculateLeadPosition(_selectedTarget);
                return;
            }

            if (_randomizeDirectionTimer.IsFinished == true)
            {
                _movePosition = UnityEngine.Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
                _randomizeDirectionTimer.Start(_randomSelectMovePointType);
            }           
        }

        private void ActionEvadeCollision()
        {
            if(Physics2D.Raycast(transform.position, transform.up, _evadeRayLength))            
                _movePosition = transform.position + transform.right * 50f;            
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
            InitTimer(ref _fireTimer, _shootDelay);
            InitTimer(ref _findNewTargetTimer, _findNewTargetTime);
        }

        private void InitTimer(ref Timer timer, float interval) => timer = new Timer();

        private void UpdateTimers()
        {
            UpdateTimer(_randomizeDirectionTimer);
            UpdateTimer(_fireTimer);
            UpdateTimer(_findNewTargetTimer);
        }

        private void UpdateTimer(Timer timer) => timer.SubstractTime(Time.deltaTime);

        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.zero, _movePosition);
            //Debug.Log(_movePosition);

            //Debug.DrawRay(transform.position, transform.up * 10);

            if (_selectedTarget != null)
                Debug.DrawLine(transform.position, _selectedTarget.transform.position, Color.red);                

        }
#endif

    }
}
