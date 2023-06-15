using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{

    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float _velocityDamageModifier;
        [SerializeField] private float _damageConstant;

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.transform.tag == IgnoreTag)
                return;
            
            if (transform.root.TryGetComponent<Destructible>(out var destructible))
            {
                int damage = (int)_damageConstant + (int)(_velocityDamageModifier * collision.relativeVelocity.magnitude);
                destructible.ApplyDamage(damage);
            }
        }

    }
}
