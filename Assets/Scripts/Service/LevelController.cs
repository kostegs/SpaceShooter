using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool _isCompleted { get; }
    }

    public class LevelController : MonoSingleton<LevelController>
    {
        [SerializeField] private int _referenceTime;
        [SerializeField] private UnityEvent _eventLevelCompleted;

        private ILevelCondition[] _conditions;
        private bool _isLevelCompleted;
        private float _levelTime;

        public int ReferenceTime => _referenceTime;
        public float LevelTime => _levelTime;
        
        void Start() => _conditions = GetComponentsInChildren<ILevelCondition>();

        void Update()
        {
            if (_isLevelCompleted == false)
            {
                _levelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (_conditions == null || _conditions.Length == 0) 
                return;

            int numCompleted = 0;

            foreach (var condition in _conditions)
            {
                if (condition._isCompleted)
                    numCompleted++;
            }

            if (numCompleted == _conditions.Length)
            {
                _isLevelCompleted = true;
                _eventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

    }
}
