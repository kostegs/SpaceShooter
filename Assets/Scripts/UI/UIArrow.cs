using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{

    public class UIArrow : MonoBehaviour
    {
        [SerializeField] private Transform _UFO;
        [SerializeField] private Camera _camera;
        [SerializeField] private TMP_Text _distanceText;
        [SerializeField] private Image _arrowImage;

        private Transform _spaceShip;
        private Vector3 _leftRotation, _rightRotation;
        private Vector3 _downRotation, _upRotation;
        private bool _arrowIsHided;

        public Vector2 DistanceFromPlayerToEnemy { get; private set; }

        private void Start()
        {
            _leftRotation = new Vector3(0f, 0f, 90f);
            _rightRotation = new Vector3(0f, 0f, -90f);
            _downRotation = new Vector3(0f, 0f, 180f);
            _upRotation = Vector3.zero;

            _spaceShip = transform.root;
        }

        private void FixedUpdate()
        {
            DistanceFromPlayerToEnemy = _UFO.position - _spaceShip.position;

            float intDistance = (int)DistanceFromPlayerToEnemy.magnitude;

            if (intDistance >= 5 && _arrowIsHided)
                ArrowVisibleState(true);
            else if (intDistance < 5 && !_arrowIsHided)
            {
                ArrowVisibleState(false);
                return;
            }
            else if (intDistance < 5 && _arrowIsHided) // Do nothing
                return;


            _distanceText.text = intDistance.ToString();

            Ray ray = new Ray(_spaceShip.position, DistanceFromPlayerToEnemy);

            // [0] = Left; [1] = Right; [2] = Down; [3] = Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            float minDistance = Mathf.Infinity;
            int planeIndex = 0;

            for (int i = 0; i < 4; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        planeIndex = i;
                    }
                }
            }

            minDistance = Mathf.Clamp(minDistance, 0, DistanceFromPlayerToEnemy.magnitude);
            Vector2 worldPosition = ray.GetPoint(minDistance - 1.5f);

            transform.position = _camera.WorldToScreenPoint(worldPosition);

            Vector3 eulerRotation = GetEulerRotation(planeIndex);

            transform.rotation = Quaternion.Euler(eulerRotation);
            _distanceText.transform.rotation = Quaternion.Euler(eulerRotation * 4);
        }

        private void ArrowVisibleState(bool state)
        {
            _distanceText.enabled = state;
            _arrowImage.enabled = state;
            _arrowIsHided = state == false;
        }

        Vector3 GetEulerRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case 0:
                    return _leftRotation;
                case 1:
                    return _rightRotation;
                case 2:
                    return _downRotation;
                case 3:
                    return _upRotation;
                default:
                    return Vector3.zero;
            }
        }

        public void TuneHud(Transform target, Camera mainCamera) => (_UFO, _camera) = (target, mainCamera);
        
    }
}
