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
        [SerializeField] private SpawnPoint[] _spawnPoints;

        private Player _playerScript;
        private int _currentSpawnPoint;

        private void Start()
        {
            RespawnSpaceShip();
            SubscribeToSpawnPoints();
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
                int RandomRotation = Random.Range(0, 180);
                newShipRotation = Quaternion.Euler(new Vector3(0, 0, RandomRotation));
            }

            SpaceShip ship = _spawner.SpawnSpaceShip(newShipPosition, newShipRotation);
            _playerScript = _playerContainer.GetComponent<Player>();

            if (ship != null)
            {
                ship.TuneHud(_target, _mainCamera);
                _playerScript.SetTarget(ship);
                ship.OnDestruct += OnSpaceShipDestruct;
                ship.ExplosionParticleSystem = _shipExplosionPS;

                _playerContainer.SetActive(true);
            }

        }

        private void SubscribeToSpawnPoints()
        {
            foreach (var spawnPoint in _spawnPoints)            
                spawnPoint.SpawnPointEnter += OnSpawnPointEnter;            
        }

        public void OnSpawnPointEnter(object spawnPoint, SpawnPointEventArgs eventArgs) => _currentSpawnPoint = eventArgs._spawnPointNumber;

    }
}
