using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    AudioSource tuto;
    private void Awake()
    {
        tuto = GetComponent<AudioSource>();
        Invoke("audiFinished", tuto.clip.length);
        tuto.Stop();
    }

    void audioFinished()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        //tuto.Play();

        if (other.tag == "Player")
        {
            tuto.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tuto.Pause();
        }
        //tuto.Pause();
    }

}

