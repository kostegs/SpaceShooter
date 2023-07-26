using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float _radius;

        public float Radius => _radius;

        private static readonly Color s_gizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmos()
        {
            Gizmos.color = s_gizmoColor;
            Gizmos.DrawSphere(transform.position, Radius);
        }
    }
}
