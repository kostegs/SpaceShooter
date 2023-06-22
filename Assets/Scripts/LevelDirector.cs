using System;
using UnityEngine;

namespace SpaceShooter
{

    public class LevelDirector : MonoBehaviour
    {
        [SerializeField] private GameObject _playerContainer;
        [SerializeField] private ObjectSpawner _spawner;
        [SerializeField] private ParticleSystem _shipExplosionPS;
        [SerializeField] private Transform _target;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LevelPoint[] _spawnPoints;
        [SerializeField] private LevelPoint _showHideArrowPoint;
        [SerializeField] private Platform _platform;

        private Player _playerScript;
        private SpaceShip _spaceShip;
        private int _currentSpawnPoint = 1;

        private void Start()
        {
            RespawnSpaceShip();
            SubscribeToSpawnPoints();
            _showHideArrowPoint.LevelPointEnter += OnShowHideArrowPointEnter;
            _platform.PlatformStartAnimation += OnPlatformStartAnimation;
        }   

        private void OnSpaceShipDestruct(object sender, System.EventArgs e)
        {
            
            if (_playerScript.NumberOfLives > 0)
                RespawnSpaceShip();
        }

        private void RespawnSpaceShip()
        {
            _playerContainer.SetActive(false);

            Vector3 newShipPosition = _spawnPoints[_currentSpawnPoint].transform.position;
            Quaternion newShipRotation = Quaternion.identity;

            if (_currentSpawnPoint == 0)
            {
                int RandomRotation = UnityEngine.Random.Range(0, 180);
                newShipRotation = Quaternion.Euler(new Vector3(0, 0, RandomRotation));
            }

            _spaceShip = _spawner.SpawnSpaceShip(newShipPosition, newShipRotation);
            _playerScript = _playerContainer.GetComponent<Player>();

            if (_spaceShip != null)
            {
                _spaceShip.TuneHud(_target, _mainCamera);
                _playerScript.SetTarget(_spaceShip);
                _spaceShip.OnDestruct += OnSpaceShipDestruct;
                _spaceShip.ExplosionParticleSystem = _shipExplosionPS;

                _playerContainer.SetActive(true);
            }

        }

        private void SubscribeToSpawnPoints()
        {
            foreach (var spawnPoint in _spawnPoints)            
                spawnPoint.LevelPointEnter += OnSpawnPointEnter;            
        }

        public void OnSpawnPointEnter(object spawnPoint,LevelPointEventArgs eventArgs) => _currentSpawnPoint = eventArgs._spawnPointNumber;

        public void OnShowHideArrowPointEnter(object ShowHideArrowPoint, LevelPointEventArgs eventArgs) => _spaceShip.ShowHideArrow();

        public void OnPlatformStartAnimation(object sender, EventArgs eventArgs)
        {
            Debug.Log("Animation started");
        }
    }
}
