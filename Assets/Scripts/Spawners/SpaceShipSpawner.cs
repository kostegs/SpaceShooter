using UnityEngine;

namespace SpaceShooter
{
    public class SpaceShipSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spaceShipPrefab;

        public PlayerSpaceShip SpawnPlayerSpaceShip(Vector3 coordinates, Quaternion rotation)
        {
            int RandomRotation = Random.Range(0, 180);

            var ship = Instantiate(_spaceShipPrefab, coordinates, rotation);

            return ship.GetComponent<PlayerSpaceShip>();
        }
    }
}
