using SpaceShooter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipSelectionController : MonoBehaviour
{
    [SerializeField] private PlayerSpaceShip _shipPrefab;

    [SerializeField] private TextMeshProUGUI _shipName;
    [SerializeField] private TextMeshProUGUI _hitPoints;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _agility;
    [SerializeField] private GameObject _selectShipPanel;
    [SerializeField] private Image _preview;

    private void Start()
    {
        if (_shipPrefab != null)
        {
            _shipName.text = _shipPrefab.NickName;
            _hitPoints.text = ($"Урон : {_shipPrefab.HitPoints.ToString()}");
            _speed.text = ($"Скорость : {_shipPrefab.MaxLinearVelocity.ToString()}");
            _agility.text = ($"Маневренность : {_shipPrefab.MaxAngularVelocity.ToString()}");
            _preview.sprite = _shipPrefab.PreviewImage;
            _preview.preserveAspect = true;
        }
    }

    public void OnSelectShip()
    {
        LevelSequenceController.PlayerShipPrefab = _shipPrefab.gameObject;
        MainMenuController.Instance.gameObject.SetActive(true);
        _selectShipPanel.gameObject.SetActive(false);
    }
}
