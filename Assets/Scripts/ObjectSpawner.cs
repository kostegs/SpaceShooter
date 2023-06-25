using SpaceShooter;
using UnityEngine;

namespace SpaceShooter
{

    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spaceShipPrefab;

        public SpaceShip SpawnSpaceShip(Vector3 coordinates, Quaternion rotation)
        {
            int RandomRotation = Random.Range(0, 180);

            var ship = Instantiate(_spaceShipPrefab, coordinates, rotation);

            return ship.GetComponent<SpaceShip>();
        }
    }
}
