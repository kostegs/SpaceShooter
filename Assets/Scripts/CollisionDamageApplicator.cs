using UnityEngine;

namespace SpaceShooter
{

    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "IgnoreCollision";

        [SerializeField] private float _velocityDamageModifier;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag || transform.tag == IgnoreTag)
                return;
            
            if ( (collision.gameObject.transform.root.TryGetComponent<Destructive>(out Destructive destructive))
                  &&
                  (transform.root.TryGetComponent<Destructible>(out var destructible)))
            {
                int damage = (int)destructive.DamagePoints + (int)(_velocityDamageModifier * collision.relativeVelocity.magnitude);
                destructible.ApplyDamage(damage);
            }            
        }

    }
}
