using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    // [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip background;
    // [SerializeField] AudioClip gameOverSFX;
    [SerializeField] float volume = 0.1f;

    private static AudioManager instance;

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        // musicSource.clip = background;
        // AudioSource.PlayClipAtPoint(background, Camera.main.transform.position, volume);

        musicSource.clip = background;
        musicSource.volume = volume;
        musicSource.loop = true; // Ensure the music loops
        musicSource.Play();
    }

//     public void PlayGameOverSFX()
//     {
//         sfxSource.clip = gameOverSFX;
//         sfxSource.volume = volume;
//         sfxSource.Play();
//     }
}
