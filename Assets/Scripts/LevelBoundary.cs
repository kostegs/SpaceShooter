using UnityEngine;

namespace SpaceShooter
{
    public enum Mode
    {
        Limit,
        Teleport
    }

    public class LevelBoundary : MonoBehaviour
    {
        [SerializeField] private float _radius;
        public float Radius { get => _radius;}

        [SerializeField] private Mode _limitMode;
        public Mode LimitMode { get => _limitMode;}

        #region Singleton
        public static LevelBoundary Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _radius);
        }  

#endif
    }
}
