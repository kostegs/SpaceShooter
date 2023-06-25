using UnityEngine;

namespace SpaceShooter
{
    public class SceneManager : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.E))
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
