using UnityEngine;

namespace SpaceShooter
{

    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Update() => transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
