using SpaceShooter;
using UnityEngine;

namespace SpaceShooter
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private int _numberOfLives;
        [SerializeField] private SpaceShip _spaceShip;
        [SerializeField] private GameObject _playerSpaceShipPrefab;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private void Start() => SubscribeToSpaceShip();

        private void OnSpaceShipDestruct(object sender, System.EventArgs e)
        {
            _numberOfLives--;

            if (_numberOfLives > 0)
                RespawnSpaceShip();
        }

        private void RespawnSpaceShip()
        {
            var newPlayerShip = Instantiate(_playerSpaceShipPrefab);

            _spaceShip = newPlayerShip.GetComponent<SpaceShip>();

            _cameraController.SetTarget(_spaceShip.transform);
            _movementController.SetTarget(_spaceShip);
            SubscribeToSpaceShip();
        }

        private void SubscribeToSpaceShip() => _spaceShip.OnDestruct += OnSpaceShipDestruct;
    }
}
