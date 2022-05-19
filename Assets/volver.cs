using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volver : MonoBehaviour
{
    public Transform Target;
    public GameObject Theplayer;



    private void OnTriggerEnter(Collider other)
    {
        Theplayer.transform.position = Target.transform.position;
        Destroy(GameObject.FindWithTag("volver"));
        Destroy(GameObject.FindWithTag("portal"));
    }
}
