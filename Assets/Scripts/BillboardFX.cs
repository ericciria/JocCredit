using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    public Transform camTransform1, camTransform3;
    public movment player;

    Quaternion originalRotation;

    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<movment>();
        originalRotation = transform.rotation;
        camTransform3 = player.cam3.transform;
        camTransform1 = player.cam1.transform;
    }

    void Update()
    {
        if (player.primeraPersona)
        {
            transform.rotation = camTransform1.rotation * originalRotation;
        }
        else
        {
            transform.rotation = camTransform3.rotation * originalRotation;
        }
        
    }
}
