using SpaceShooter;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spaceShipPrefab;

    public SpaceShip SpawnSpaceShip()
    {
        var ship = Instantiate(_spaceShipPrefab);

        return ship.GetComponent<SpaceShip>();
    }
}
