using SpaceShooter;
using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int _spawnPointNumber;

    public event EventHandler<SpawnPointEventArgs> SpawnPointEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponentInParent<SpaceShip>() != null)            
            SpawnPointEnter?.Invoke(this, new SpawnPointEventArgs(_spawnPointNumber));        
            
    }
}
