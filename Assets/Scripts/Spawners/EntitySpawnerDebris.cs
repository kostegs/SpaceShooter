using System;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] _debrisPrefabs;
        [SerializeField] private CircleArea _circleArea;
        [SerializeField] private int _numberDebris;        
        [SerializeField] private float _randomSpeed;
        
        private void Start()
        {
            foreach (var debris in _debrisPrefabs)
            {
                for (int i = 0; i <= _numberDebris; i++)
                    SpawnDebris();
            }            
        }

        private void SpawnDebris()
        {
            int index = UnityEngine.Random.Range(0, _debrisPrefabs.Length);

            GameObject debris = Instantiate(_debrisPrefabs[index]).gameObject;
            
            debris.transform.position = _circleArea.GetRandomInsideZone();

            Destructible destructible = debris.GetComponent<Destructible>();
            destructible.OnDestruct += OnDebrisDead;            

            Rigidbody2D rb = debris.GetComponent <Rigidbody2D>();

            if (rb != null && _randomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * _randomSpeed;
            }
        }

        private void OnDebrisDead(object Object, EventArgs e) => SpawnDebris();

    }
}
