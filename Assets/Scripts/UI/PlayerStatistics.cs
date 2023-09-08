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
    }
}
