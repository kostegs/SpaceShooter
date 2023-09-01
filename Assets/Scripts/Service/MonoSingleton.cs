using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Singleton for MonoBehaviours
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DisallowMultipleComponent]
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Automatically mark object as persistent.
        /// </summary>
        [Header("Singleton")]
        [SerializeField] private bool _doNotDestroyOnLoad;

        /// <summary>
        /// Singleton instance. May be null if DoNotDestroyOnLoad flag was not set.
        /// </summary>
        public static T Instance { get; private set; }

        #region Unity events

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed=" + typeof(T).Name);
                Destroy(this);
                return;
            }

            Instance = this as T;

            if (_doNotDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}
