using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        if (!collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }  
    }
}
