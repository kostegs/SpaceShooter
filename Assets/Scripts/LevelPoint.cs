using System;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelPoint : MonoBehaviour
    {
        [SerializeField] private int _spawnPointNumber;

        public event EventHandler<LevelPointEventArgs> LevelPointEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponentInParent<PlayerSpaceShip>() != null)
                LevelPointEnter?.Invoke(this, new LevelPointEventArgs(_spawnPointNumber));
        }
    }
}
