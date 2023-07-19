using SpaceShooter;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupSpawner : EntitySpawner
    {
        [SerializeField] private Powerup[] _powerUpPrefabs;

        internal override void SpawnEntities()
        {
            for (int i = 0; i < _numSpawns; i++)
            {
                int index = Random.Range(0, _powerUpPrefabs.Length);

                GameObject entity = Instantiate(_powerUpPrefabs[index]).gameObject;
                entity.transform.position = _circleArea.GetRandomInsideZone();
            }
        }
    }
}
