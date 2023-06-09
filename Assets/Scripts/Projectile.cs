using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _damage;
        [SerializeField] private ImpactEffect _impactEffectPrefab;

        private float _timer;
        private Destructible _parent;

        private void Update()
        {
            float stepLength = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                if (hit.collider.transform.root.TryGetComponent<Destructible>(out Destructible dest) && dest != _parent)
                {
                    dest.ApplyDamage(_damage);
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }
            
            _timer += Time.deltaTime;

            if (_timer > _lifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        public void SetParrentShooter(Destructible parent) => _parent = parent;

    }
}
