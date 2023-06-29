using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip _targetShip;
        [SerializeField] private VirtualJoystick _joystick;
        [SerializeField] private PointerClickHold _mobileFirePrimary;
        [SerializeField] private PointerClickHold _mobileFireSecondary;

        private ControlMode _controlMode;

        private void Start()
        {
            SwitchControlMode(ControlMode.Keyboard);
            _joystick.OnJoystickUsed += OnJoystickUsedHandler;
            _joystick.OnJoystickUseFinished += OnJoystickUseFinishedHandler;

            bool mobileMode = _controlMode == ControlMode.Mobile;
            
            _joystick.gameObject.SetActive(mobileMode);
            _mobileFirePrimary.gameObject.SetActive(mobileMode);
            _mobileFireSecondary.gameObject.SetActive(mobileMode);            
        }

        private void Update()
        {
            if (_targetShip == null)
                return;

            if (_controlMode == ControlMode.Keyboard)
                ControlKeyboard();
            else if (_controlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void SwitchControlMode(ControlMode controlMode) => _controlMode = controlMode;            

        public void OnJoystickUsedHandler() => SwitchControlMode(ControlMode.Mobile);

        public void OnJoystickUseFinishedHandler() => SwitchControlMode(ControlMode.Keyboard);

        private void ControlMobile()
        {
            Vector2 direction = _joystick.Value;

            var dotUp = Vector2.Dot(direction, _targetShip.transform.up);
            var dotRight = Vector2.Dot(direction, _targetShip.transform.right);

            if (_mobileFirePrimary.IsHold)
                _targetShip.Fire(TurretMode.Primary);

            if (_mobileFireSecondary.IsHold)
                _targetShip.Fire(TurretMode.Primary);

            _targetShip.ThrustControl = Mathf.Max(0, dotUp);
            _targetShip.TorqueControl = -dotRight;
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.W))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.S))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.D))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.A))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.Space))
                _targetShip.Fire(TurretMode.Primary);

            if (Input.GetKey(KeyCode.X))
                _targetShip.Fire(TurretMode.Primary);



            _targetShip.ThrustControl = thrust;
            _targetShip.TorqueControl = torque;

        }

        public void SetTarget(SpaceShip ship) => _targetShip = ship;

    }

}
