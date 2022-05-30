using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scriptMenu : MonoBehaviour
{
    public Button Options;
    public Button Exit;
    public Button Jugar;
    public Button continuar;
    public Button opcions;
    public Button tornar;
    public Button cargar;


    public void Play()
    {
        Options.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        Jugar.gameObject.SetActive(false);
        continuar.gameObject.SetActive(true);
        tornar.gameObject.SetActive(true);
        cargar.gameObject.SetActive(true);

    }

    void Start()
    {
        if (continuar != null)
        {
            continuar.gameObject.SetActive(false);
        }
        if (tornar != null)
        {
            tornar.gameObject.SetActive(false);
        }
        if (cargar != null)
        {
            cargar.gameObject.SetActive(false);
        }
    }


    public void FunctionTornar()
    {

        Options.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        Jugar.gameObject.SetActive(true);
        continuar.gameObject.SetActive(false);
        tornar.gameObject.SetActive(false);
        cargar.gameObject.SetActive(false);
    }
    public void ActivarMapa()
    {
        SceneManager.LoadScene("provesEric");
    }


    public void ActivarMenu()
    {
        SceneManager.LoadScene("play");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
