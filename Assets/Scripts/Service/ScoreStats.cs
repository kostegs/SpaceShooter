using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private int _lastScore;

        void Update() => UpdateScore();

        private void UpdateScore()
        {
            if (Player.Instance != null && Player.Instance.Score != _lastScore)
            {
                _lastScore = Player.Instance.Score;
                _text.text = $"Кол-во очков: {_lastScore.ToString()}";
            }
        }
    }
}
