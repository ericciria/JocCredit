using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;
    private AudioSource[] allAudioSources ;
    PlayerController player;
    movment mov;

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
        pauseMenu.SetActive(false);
        mov = GameObject.Find("/Player").GetComponent<movment>();
    }


  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (!player.isDead)
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    StopAllAudio();
                }
                Time.timeScale = isPaused ? 0 : 1;
                pauseMenu.SetActive(isPaused);
                mov.isPaused = isPaused;
            }
            
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

   public  void Continuar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        mov.isPaused = isPaused;
    }
}
 