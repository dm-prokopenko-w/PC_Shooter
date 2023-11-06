using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Game.Core
{
    public class SceneLoader 
    {
        public void LoadMenuScene()
        {
            SceneManager.LoadScene("Menu");
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene("Game");
        }

        public void LoadScene(int sceneIndex)
        {
            if (SceneManager.sceneCountInBuildSettings > sceneIndex)
            {
                SceneManager.LoadSceneAsync(sceneIndex);
            }
            else
            {
                Debug.LogError("There is no scene index - " + sceneIndex);
            }
        }
    }
}