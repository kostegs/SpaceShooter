using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class UISpaceShipAmmo : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceShip _ship;
        [SerializeField] private TextMeshProUGUI _ammoText;

        private void Start()
        {
            _ship.OnAmmoChanged += OnSpaceShipAmmoChanged;
            _ammoText.text = GetAmmoText();
        }

        private void OnSpaceShipAmmoChanged(object sender, System.EventArgs e) => _ammoText.text = GetAmmoText();

        private string GetAmmoText()
        {
            int spaceShipAmmo = _ship.CurrentAmmo < 0 ? 0 : _ship.CurrentAmmo;
            return spaceShipAmmo.ToString();
        }
    }
}
