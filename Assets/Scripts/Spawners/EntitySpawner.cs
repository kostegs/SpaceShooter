using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] _entityPrefabs;
        [SerializeField] private CircleArea _circleArea;
        [SerializeField] private SpawnMode _spawnMode;
        [SerializeField] private int _numSpawns;
        [SerializeField] private float _respawnTime;

        private float _timer;

        private void Start()
        {
            if (_spawnMode == SpawnMode.Start)            
                SpawnEntities();
            
            _timer = _respawnTime;
        }

        private void Update()
        {
            if (_timer > 0)
                _timer -= Time.deltaTime;
            
            if (_spawnMode == SpawnMode.Loop && _timer < 0)
            {
                SpawnEntities();
                _timer = _respawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < _numSpawns; i++)
            {
                int index = Random.Range(0, _entityPrefabs.Length);

                GameObject entity = Instantiate(_entityPrefabs[index]).gameObject;
                entity.transform.position = _circleArea.GetRandomInsideZone();
            }
        }
    }
}
