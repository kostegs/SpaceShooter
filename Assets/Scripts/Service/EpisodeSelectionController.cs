using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{

    public class EpisodeSelectionController : MonoBehaviour
    {
        [SerializeField] private Episode _episode;
        [SerializeField] private TextMeshProUGUI _episodeNickName;
        [SerializeField] private Image _previewImage;

        private void Start()
        {
            if (_episodeNickName != null)            
                _episodeNickName.text = _episode.EpisodeName;

            if (_previewImage != null)
                _previewImage.sprite = _episode.PreviewImage;

        }

        public void OnStartEpisodeButtonClicked() => LevelSequenceController.Instance.StartEpisode(_episode);

    }
}
