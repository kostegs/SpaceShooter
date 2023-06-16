using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private float _interpolationLinearSpeed;
        [SerializeField] private float _interpolationAngularSpeed;
        [SerializeField] private float _cameraZOffset;
        [SerializeField] private float _forwardOffset;

        private void FixedUpdate()
        {
            if (_target == null || _camera == null)
                return;

            // Moving
            Vector2 camPos = _camera.transform.position;
            Vector2 targetPos = _target.position + (_target.up * _forwardOffset);
            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, _interpolationLinearSpeed * Time.fixedDeltaTime);

            _camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, _cameraZOffset);

            // Rotation
            if (_interpolationAngularSpeed > 0)
                _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _target.rotation, _interpolationAngularSpeed * Time.fixedDeltaTime);
        }

        public void SetTarget(Transform target) => _target = target;

    }
}
