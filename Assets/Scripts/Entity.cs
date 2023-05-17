using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Base class for all interactive objects.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// The name of the object for an user.
        /// </summary>
        [SerializeField] private string _nickName;

        public string NickName => _nickName;
    }
}
