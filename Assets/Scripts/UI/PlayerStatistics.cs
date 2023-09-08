using UnityEngine;

namespace SpaceShooter
{

    public class PlayerStatistics : MonoBehaviour
    {
        [SerializeField] private int _numKills;
        [SerializeField] private int _score;
        [SerializeField] private int _time;

        public int Kills => _numKills;
        public int Score => _score;
        public int Time => _time;

        public void Reset()
        {
            _numKills = 0;
            _score = 0;
            _time = 0;
        }

        public void CalculateLevelStatistics()
        {
            _score = Player.Instance.Score;
            _numKills = Player.Instance.NumKills;
            _time = (int)LevelController.Instance.LevelTime;
        }
        
    }
}
