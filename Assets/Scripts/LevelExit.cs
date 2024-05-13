using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    //     [SerializeField] float levelLoadDely = 0.5f;
    void OnTriggerEnter2D(Collider2D other)
    {
        // StartCoroutine(LoadNextLevel());
        if (other.tag == "Player")
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(nextSceneIndex);
            FindObjectOfType<GameSession>().ResetNumberOfCoins();
        }
    }

    //     IEnumerator LoadNextLevel()
    //     {
    //         yield return new WaitForSecondsRealtime(levelLoadDely);

    //         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //         SceneManager.LoadScene(currentSceneIndex + 1);
    //     }
}
