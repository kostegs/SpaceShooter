using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class UISpaceShipSpeed : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceShip _ship;
        [SerializeField] private TextMeshProUGUI _timerText;

        private void Start() => _ship.TimerSpeedChanged += OnTimerSpeedChanged;

        private void OnTimerSpeedChanged(object sender, TimerEventArgs e) => _timerText.text = e.Timer.ToString();
    }
}
