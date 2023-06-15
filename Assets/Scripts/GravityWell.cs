using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null)
                return;

            Vector2 direction = transform.position - collision.transform.position;

            float distance = direction.magnitude;
            
            Vector2 force = direction.normalized * _force * (distance / _radius);
            collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
        }

#if UNITY_EDITOR
        private void OnValidate() => GetComponent<CircleCollider2D>().radius = _radius;
#endif
    }
}
