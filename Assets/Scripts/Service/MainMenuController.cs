using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : MonoSingleton<MainMenuController>
    {
        [SerializeField] private GameObject _defaultSpaceShip;
        [SerializeField] private GameObject _episodeSelection;
        [SerializeField] private GameObject _shipSelectionPanel;

        private void Start() => LevelSequenceController.PlayerShipPrefab = _defaultSpaceShip;

        public void OnButtonStartNew()
        {
            _episodeSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonSelectSpaceShip()
        {
            _shipSelectionPanel.SetActive(true);
            gameObject.SetActive(false);
        }  

        public void OnButtonExit() => Application.Quit();
    }
}
