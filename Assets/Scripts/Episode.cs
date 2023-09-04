using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        [SerializeField] private string _episodeName;
        [SerializeField] private string[] _levels;
        [SerializeField] private Sprite _previewImage;

        public string EpisodeName => _episodeName;
        public string[] Levels => _levels;
        public Sprite PreviewImage => _previewImage;
    }
}
