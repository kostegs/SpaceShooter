using UnityEngine;

namespace SpaceShooter
{
    public class SpaceShipSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spaceShipPrefab;

        public PlayerSpaceShip SpawnPlayerSpaceShip(GameObject spaceShipPrefab, Vector3 coordinates, Quaternion rotation)
        {
            int RandomRotation = Random.Range(0, 180);

            var ship = Instantiate(spaceShipPrefab, coordinates, rotation);

            return ship.GetComponent<PlayerSpaceShip>();
        }

        public PlayerSpaceShip SpawnPlayerSpaceShip(Vector3 coordinates, Quaternion rotation) =>
            SpawnPlayerSpaceShip(_spaceShipPrefab, coordinates, rotation);
    }
}
