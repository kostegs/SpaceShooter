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

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            // сбрасываем статы перед началом эпизода

            UnityEngine.SceneManagement.SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool succes)
        {
            if (succes)            
                AdvanceLevel();            
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
                UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuSceneNickName);            
            else            
                UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);            
        }

    }
}
