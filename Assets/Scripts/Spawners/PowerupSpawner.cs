using SpaceShooter;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace SpaceShooter
{
    public class PowerupSpawner : EntitySpawner
    {
        [SerializeField] private Powerup[] _powerUpPrefabs;

        internal override void SpawnEntities()
        {
            int index = 0;

            for (int i = 0; i < _numSpawns; i++)
            {
                if (index == _powerUpPrefabs.Length)
                    index = 0;

                GameObject entity = Instantiate(_powerUpPrefabs[index++]).gameObject;
                entity.transform.position = _circleArea.GetRandomInsideZone();
            }
        }
    }
}
