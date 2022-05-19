using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform Target;
    public GameObject Theplayer;
    public GameObject cam3;
    public GameObject cam1;
    AudioSource tuto;
    private void Awake()
    {
        tuto = GetComponent<AudioSource>();
        tuto.Stop();
    }


    private void OnTriggerEnter(Collider other)
    {
        Theplayer.transform.position = Target.transform.position;
        tuto.Play();

        if (other.tag == "Player")
        {
            Debug.Log("HOla");
            cam3.SetActive(false);
            cam1.SetActive(false);
        }




    }

   
}

