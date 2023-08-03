
using UnityEngine;

namespace SpaceShooter
{
    public enum AsteroidSize
    {
        Big,
        Small
    }

    public class Asteroid : Destructible
    {
        [SerializeField] private AsteroidSize _size;
        [SerializeField] private Asteroid _asteroidPartPrefab;
                
        protected override void OnDeath()
        {
            switch (_size)
            {
                case AsteroidSize.Big:
                    for (int i = 0; i < 3; i++)
                    {
                        Asteroid part = Instantiate(_asteroidPartPrefab, transform.position, Quaternion.identity) as Asteroid;

                        Rigidbody2D rb = part.GetComponent<Rigidbody2D>();

                        if (rb != null)
                            rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere;
                    }
                    ObjectDestroyer.Instance.DestroyGameObject(gameObject, 1.5f, DestroyEffect, _spriteRenderer, base.OnDeath);
                    break;
                case AsteroidSize.Small:
                    ObjectDestroyer.Instance.DestroyGameObject(gameObject, 1.5f, DestroyEffect, _spriteRenderer, null);
                    break;
            }                
        }
    }
}
