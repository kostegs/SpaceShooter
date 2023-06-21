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

        private Player _playerScript;

        private void Start()
        {
            RespawnSpaceShip();
        }   

        private void OnSpaceShipDestruct(object sender, System.EventArgs e)
        {
            
            if (_playerScript.NumberOfLives > 0)
                RespawnSpaceShip();
        }

        private void RespawnSpaceShip()
        {
            _playerContainer.SetActive(false);
            SpaceShip ship = _spawner.SpawnSpaceShip();
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

    }
}
