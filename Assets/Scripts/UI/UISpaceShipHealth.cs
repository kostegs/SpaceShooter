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
            ChangeHealthText();
        }

        private void OnSpaceShipHit(object sender, System.EventArgs e) => ChangeHealthText();                 

        private void ChangeHealthText()
        {
            int GetHP()
            {
                if (_ship.CurrentHitPoints > 100)
                    return _ship.CurrentHitPoints / 100;
                else
                    return (_ship.CurrentHitPoints < 0 ? 0 : _ship.CurrentHitPoints);
            }

            _healthText.text = GetHP().ToString();
        }

    }
}
