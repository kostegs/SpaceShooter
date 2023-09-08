using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : MonoSingleton<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI _kills;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private TextMeshProUGUI _result;
        [SerializeField] private TextMeshProUGUI _buttonNextText;

        private bool _success;

        private void Start() => gameObject.SetActive(false);

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);
            
            _success = success;
            _result.text = _success ? "Победа" : "Поражение";
            _buttonNextText.text = success ? "Следующий уровень" : "Начать заново";

            Time.timeScale = 0f;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1.0f;

            if (_success)
                LevelSequenceController.Instance.AdvanceLevel();
            else
                LevelSequenceController.Instance.RestartLevel();

        }
    }
}
