using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Class used for locking position. It's work only with LevelBoundary script, if it's on a scene. 
    /// Use it with object with need to be locked.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null)
                return;

            var lb = LevelBoundary.Instance;
            var radius = lb.Radius;

            if (transform.position.magnitude > radius)
            {
                if (lb.LimitMode == Mode.Limit)
                    transform.position = transform.position.normalized * radius;
                else if (lb.LimitMode == Mode.Teleport)
                    transform.position = -(transform.position.normalized * radius);                
            }
        }
    }

}
