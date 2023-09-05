using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _episodeSelection;

        public void OnButtonStartNew()
        {
            _episodeSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonExit() => Application.Quit();
    }
}
