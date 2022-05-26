using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;
    private AudioSource[] allAudioSources ;

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        pauseMenu.SetActive(isPaused);
    }


    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                StopAllAudio();
            }
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }
    }

    void StopAllAudio()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS != null)
            {
                audioS.Pause();
            }
        }
    }
}
 