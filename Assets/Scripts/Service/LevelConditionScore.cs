using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int _score;

        private bool _reached;

        public bool _isCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= _score)
                    {
                        _reached = true;
                        Debug.Log("Reached");
                    }
                }

                return _reached;
            }
        }
    }
}
