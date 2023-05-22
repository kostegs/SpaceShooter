using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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
        [SerializeField] private ControlMode _controlMode;
        [SerializeField] private bool _autoControlMode;

        private void Start()
        {
            if (_autoControlMode)
                SwitchControlMode();

            ManageVisibility();
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

        private void SwitchControlMode()
        {
            if (Application.isMobilePlatform)            
               _controlMode = ControlMode.Mobile;            
            else            
               _controlMode = ControlMode.Keyboard;            
        }

        private void ManageVisibility()
        {
            if (_controlMode == ControlMode.Mobile)                            
                _joystick.gameObject.SetActive(true);            
            else                
                _joystick.gameObject.SetActive(false);            
        }

        private void ControlMobile()
        {
            Vector2 direction = _joystick.Value;

            var dot = Vector2.Dot(direction, _targetShip.transform.up);
            var dot2 = Vector2.Dot(direction, _targetShip.transform.right);

            _targetShip.ThrustControl = Mathf.Max(0, dot);
            _targetShip.TorqueControl = -dot2;
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
