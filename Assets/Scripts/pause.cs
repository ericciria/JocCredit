using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;
    private AudioSource[] allAudioSources ;
    PlayerController player;
    movment mov;
    Scene escena;

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
        pauseMenu.SetActive(false);
        mov = GameObject.Find("/Player").GetComponent<movment>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        escena = SceneManager.GetActiveScene();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        escena = scene;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escena.name.Equals("play"))
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
 