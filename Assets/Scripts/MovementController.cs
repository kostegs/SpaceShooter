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

        private ControlMode _controlMode;

        private void Start()
        {
            SwitchControlMode(ControlMode.Keyboard);
            _joystick.OnJoystickUsed += OnJoystickUsedHandler;
            _joystick.OnJoystickUseFinished += OnJoystickUseFinishedHandler;

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

            _targetShip.ThrustControl = direction.y;
            _targetShip.TorqueControl = -direction.x;
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

            _targetShip.ThrustControl = thrust;
            _targetShip.TorqueControl = torque;

        }
    }

}
