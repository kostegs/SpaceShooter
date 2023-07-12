using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelDirector : MonoBehaviour
    {
        [SerializeField] private GameObject _playerContainer;
        [SerializeField] private ObjectSpawner _spawner;
        [SerializeField] private ParticleSystem _shipExplosionPS;
        [SerializeField] private Transform _target;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LevelPoint[] _spawnPoints;

        [Header("UFO Platform Animation")]
        [SerializeField] private Platform _platform;
        [SerializeField] private Animator _gatesAnimator;
        [SerializeField] private Animator _platformAnimator;
        [SerializeField] private SpriteRenderer _enterPlatformSpriteRenderer;
        [SerializeField] private LevelPoint _shipInPlatformCentre;
        [SerializeField] private LevelPoint _shipIntoUFO;

        [Header("End Platform Animation")]
        [SerializeField] private Platform _endPlatform;
        [SerializeField] private Collider2D _wallCollider;
        [SerializeField] private LevelPoint _endLevelPoint;        
        [SerializeField] private Image _blackScreenImage;

        private Player _playerScript;
        private SpaceShip _spaceShip;
        private int _currentSpawnPoint = 0;
        

        private void Start()
        {
            StartCoroutine(SetBlackScreen(1, 0, false));            
            RespawnSpaceShip();
            SubscribeToSpawnPoints();            
            _platform.PlatformStartAnimation += OnPlatformStartAnimation;
            _endPlatform.PlatformStartAnimation += OnEndPlatformStartAnimation;
        }   

        private void OnSpaceShipDestruct(object sender, System.EventArgs e)
        {
            
            if (_playerScript.NumberOfLives > 0)
                RespawnSpaceShip();
        }

        private void RespawnSpaceShip()
        {
            _playerContainer.SetActive(false);

            Vector3 newShipPosition = _spawnPoints[_currentSpawnPoint].transform.position;
            Quaternion newShipRotation = Quaternion.identity;

            if (_currentSpawnPoint == 0)
            {
                int RandomRotation = UnityEngine.Random.Range(0, 180);
                newShipRotation = Quaternion.Euler(new Vector3(0, 0, RandomRotation));
            }

            _spaceShip = _spawner.SpawnSpaceShip(newShipPosition, newShipRotation);
            _playerScript = _playerContainer.GetComponent<Player>();

            if (_spaceShip != null)
            {
                _spaceShip.TuneHud(_target, _mainCamera);                
                _playerScript.SetTarget(_spaceShip);
                _spaceShip.OnDestruct += OnSpaceShipDestruct;
                _spaceShip.ExplosionParticleSystem = _shipExplosionPS;

                if (_currentSpawnPoint >= 2)
                    _spaceShip.GetComponentInChildren<UIArrow>().gameObject.SetActive(false);

                _playerContainer.SetActive(true);
            }

        }

        private void SubscribeToSpawnPoints()
        {
            foreach (var spawnPoint in _spawnPoints)            
                spawnPoint.LevelPointEnter += OnSpawnPointEnter;            
        }

        public void OnSpawnPointEnter(object spawnPoint,LevelPointEventArgs eventArgs) => _currentSpawnPoint = eventArgs._spawnPointNumber;
                
        public void OnPlatformStartAnimation(object sender, EventArgs eventArgs) => StartCoroutine(OpenGatesAnimation());

        public void OnEndPlatformStartAnimation(object sender, EventArgs eventArgs) => StartCoroutine(EndLevelAnimation());

        IEnumerator OpenGatesAnimation()
        {
            _playerScript.gameObject.SetActive(false);
            _gatesAnimator.enabled = true;
            _platformAnimator.enabled = true;
            yield return new WaitForSeconds(5);
            yield return StartCoroutine(SetAlfa());

            Vector2 goalPosition = _shipInPlatformCentre.transform.position;
            float estimatedTime = 1.5f;
            yield return StartCoroutine(MoveShipIntoUFO(goalPosition, estimatedTime));

            estimatedTime = 3.0f;
            goalPosition = _shipIntoUFO.transform.position;
            yield return StartCoroutine(MoveShipIntoUFO(goalPosition, estimatedTime));

            _spaceShip.GetComponentInChildren<UIArrow>().enabled = false;
            _playerScript.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            // return gates to normal state
            var animatorState = _gatesAnimator.GetCurrentAnimatorStateInfo(0);
            _gatesAnimator.Play(animatorState.shortNameHash, -1, 0f);

            yield return new WaitForSeconds(1);
            _gatesAnimator.enabled = false;

        }

        IEnumerator MoveShipIntoUFO(Vector2 goalPosition, float timeForMoving)
        {
            Transform shipTransform = _spaceShip.transform;
            shipTransform.rotation = Quaternion.identity;

            Vector2 startPos = shipTransform.position;            
            float elapsedTime = 0;            
            float delta = 0;
            
            while (elapsedTime < timeForMoving)
            {
                delta = elapsedTime / timeForMoving;

                shipTransform.position = Vector2.Lerp(startPos, goalPosition, delta);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            shipTransform.position = goalPosition;            
        }

        IEnumerator SetAlfa()
        {
            float elapsedTime = 0;
            float duration = 3.0f;
            float startValue = _enterPlatformSpriteRenderer.color.a;

            Color color = _enterPlatformSpriteRenderer.color;

            while (elapsedTime < duration)
            {
                color.a = Mathf.Lerp(startValue, 0, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                _enterPlatformSpriteRenderer.color = color;
                yield return null;
            }

            color.a = 0;
            _enterPlatformSpriteRenderer.color = color;
        }

        IEnumerator EndLevelAnimation()
        {
            _playerScript.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            _wallCollider.enabled = false;

            Vector2 goalPosition = _endLevelPoint.transform.position;
            float estimatedTime = 3.0f;
            yield return StartCoroutine(MoveShipIntoUFO(goalPosition, estimatedTime));

            _spaceShip.GetComponentInChildren<Light2D>().enabled = false;

            _blackScreenImage.gameObject.SetActive(true);
            yield return StartCoroutine(SetBlackScreen(0, 1, true));
        }

        IEnumerator SetBlackScreen(float startValue, float endValue, bool activeInTheEnd)
        {
            float elapsedTime = 0;
            float estimatedTime = 3.0f;
            Color imageColor = _blackScreenImage.color;

            while (elapsedTime < estimatedTime)
            {
                imageColor.a = Mathf.Lerp(startValue, endValue, elapsedTime / estimatedTime);
                _blackScreenImage.color = imageColor;
                elapsedTime += Time.deltaTime;                
                yield return null;
            }

            imageColor.a = endValue;
            _blackScreenImage.color = imageColor;
            _blackScreenImage.gameObject.SetActive(activeInTheEnd);
        }
    }
}
