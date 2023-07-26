using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class UISpaceShipEnergy : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceShip _ship;
        [SerializeField] private TextMeshProUGUI _energyText;

        private void Start()
        {
            _ship.OnEnergyChanged += OnSpaceShipEnergyChanged;
            _energyText.text = GetEnergyText();
        }

        private void OnSpaceShipEnergyChanged(object sender, System.EventArgs e) => _energyText.text = GetEnergyText();

        private string GetEnergyText()
        {
            float spaceShipEnergy = _ship.CurrentEnergy < 0 ? 0 : _ship.CurrentEnergy;
            return Mathf.Round(spaceShipEnergy).ToString();
        }
    }
}
