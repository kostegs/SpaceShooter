using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class AutoGoalRocket : Projectile
    {
        private Vector3 _enemyPosition;

        private void Start() => StartCoroutine(AutoGoal());            

        void Update()
        {
            float stepLength = Time.deltaTime * _velocity;
            Vector2 step = transform.up * stepLength;
             
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);            

            if (hit)
            {
                if (hit.collider.transform.root.TryGetComponent<Destructible>(out Destructible dest) && dest != _parent)
                    dest.ApplyDamage(_damage);

                OnProjectileLifeEnd(hit.collider, hit.point);
            }           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.root.TryGetComponent<Destructible>(out Destructible dest) && dest != _parent)
            {
                _enemyPosition = collision.gameObject.transform.position;
                GetComponent<Collider2D>().enabled = false;
            }
        }

        IEnumerator AutoGoal()
        {
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

        private void MoveRocket()
        {
            float stepLength = Time.deltaTime * _velocity;
            transform.position += (transform.up * stepLength);

            _timer += Time.deltaTime;

            if (_timer > _lifeTime)
                Destroy(gameObject);
        }

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
