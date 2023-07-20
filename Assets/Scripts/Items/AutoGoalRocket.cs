using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class AutoGoalRocket : Projectile
    {
        private Vector3 _enemyPosition;

        private void Start() => StartCoroutine(AutoGoal());            

        void Update() => MakeDamageToDestructibleObject(); 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.root.TryGetComponent<Destructible>(out Destructible dest) && dest.GetComponent<SpaceShip>() != _parent)
            {
                _enemyPosition = collision.gameObject.transform.position;
                GetComponent<Collider2D>().enabled = false;
            }
        }

        IEnumerator AutoGoal()
        {
            Invoke("OnProjectileLifeEnd", _lifeTime);
            yield return StartCoroutine(Moving());
            yield return StartCoroutine(MovingToGoal());

        }
        IEnumerator Moving()
        {
            while (_enemyPosition == Vector3.zero)
            {
                MoveRocket();
                yield return null;
            }

            yield return StartCoroutine(Rotation());

        }

        IEnumerator MovingToGoal()
        {
            _velocity = 20;

            while (true)
            {
                MoveRocket();                
                yield return null;
            }
        }

        private void MoveRocket() => transform.position += (transform.up * Time.deltaTime * _velocity);            

        IEnumerator Rotation()
        {
            float timeForRotation = 0.01f;
            float maxTurnSpeed = 180;
            float targetAngle = Vector2.SignedAngle(transform.up, (_enemyPosition - transform.position));
            targetAngle += transform.eulerAngles.z;

            float currentVelocity = 0, angle = transform.eulerAngles.z;

            while (Mathf.Floor(angle) != Mathf.Floor(targetAngle))
            {
                angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, timeForRotation, maxTurnSpeed);
                transform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }

            transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }


    }
}
