using UnityEngine;

namespace SpaceShooter
{
    public abstract class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] internal CircleArea _circleArea;
        [SerializeField] internal SpawnMode _spawnMode;
        [SerializeField] internal int _numSpawns;
        [SerializeField] internal float _respawnTime;

        internal float _timer;

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

        internal abstract void SpawnEntities();
        
    }
}
