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
        [SerializeField] private ParticleSystem _shipExplosionPS;
        
        private void Start()
        {
            SetExplosionEffectToShip();
            SubscribeToSpaceShip();
        }

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
            SetExplosionEffectToShip();
            SubscribeToSpaceShip();
        }

        private void SubscribeToSpaceShip() => _spaceShip.OnDestruct += OnSpaceShipDestruct;

        private void SetExplosionEffectToShip() => _spaceShip.ExplosionParticleSystem = _shipExplosionPS;
    }
}
