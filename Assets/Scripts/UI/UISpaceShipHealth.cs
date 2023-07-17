using TMPro;
using UnityEngine;

namespace SpaceShooter
{

    public class UISpaceShipHealth : MonoBehaviour
    {
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private TextMeshProUGUI _healthText;

        private void Start()
        {
            _ship.OnHit += OnSpaceShipHit;
            _healthText.text = _ship.CurrentHitPoints.ToString();
        }

        private void OnSpaceShipHit(object sender, System.EventArgs e) => _healthText.text = (_ship.CurrentHitPoints < 0 ? 0 : _ship.CurrentHitPoints).ToString();
    }
}
