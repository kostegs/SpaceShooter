using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : MonoSingleton<LevelSequenceController>
    {
        public static string MainMenuSceneNickName = "MainMenu";
        public static GameObject PlayerShipPrefab { get; set; }

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }
        
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics LevelStatistics { get; private set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            // сбрасываем статы перед началом эпизода
            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            UnityEngine.SceneManagement.SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistics();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);

            if (success)            
                AdvanceLevel();            
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
                UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuSceneNickName);            
            else            
                UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);            
        }

        private void CalculateLevelStatistics() => LevelStatistics.CalculateLevelStatistics();
    }
}
