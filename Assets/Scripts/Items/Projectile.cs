using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] internal float _velocity;
        [SerializeField] internal float _lifeTime;
        [SerializeField] internal int _damage;
        [SerializeField] private ImpactEffect _impactEffectPrefab;

        internal float _timer;
        internal Destructible _parent;

        private void Start() => Invoke("OnProjectileLifeEnd", _lifeTime);

        private void Update()
        {
            MakeDamageToDestructibleObject();

            float stepLength = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLength;
            
            transform.position += new Vector3(step.x, step.y, 0);
        }

        internal void MakeDamageToDestructibleObject()
        {           

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Time.deltaTime * _velocity);

            if (hit)
            {
                if (hit.collider.transform.root.TryGetComponent<Destructible>(out Destructible dest) && dest.GetComponent<SpaceShip>() != _parent)
                {
                    dest.ApplyDamage(_damage);
                    OnProjectileLifeEnd();
                }
                else if (hit.collider.transform.root.TryGetComponent<Destructive>(out Destructive destructive) && destructive.GetComponent<SpaceShip>() != _parent)
                {
                    OnProjectileLifeEnd();
                }
            }           
        }

        protected void OnProjectileLifeEnd() => Destroy(gameObject);

        public void SetParrentShooter(Destructible parent) => _parent = parent;

    }
}
