using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    [SerializeField] int numberOfCoins = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;

    AudioManager audioManager;

    void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        coinsText.text = numberOfCoins.ToString();

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayBackgroundMusic();


    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToCoins(int pointsToAdd)
    {
        numberOfCoins += pointsToAdd;
        coinsText.text = numberOfCoins.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(4);
        Destroy(gameObject);
    }

    public void ResetNumberOfCoins()
    {
        numberOfCoins = 0;
        coinsText.text = numberOfCoins.ToString();
    }

}
