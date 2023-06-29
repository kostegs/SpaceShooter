using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        void Start() => StartCoroutine(DestroyAfterLifeTime());

        IEnumerator DestroyAfterLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}