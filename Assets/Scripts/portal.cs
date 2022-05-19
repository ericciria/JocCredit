using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform Target;
    public GameObject Theplayer;
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
    }
}

