using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<SpaceShip>(out SpaceShip ship))
            {
                OnPickedUp(ship);
                Destroy(gameObject);
            }            
        }

        protected abstract void OnPickedUp(SpaceShip ship);
    }
}
