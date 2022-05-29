using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPlay : MonoBehaviour
{
    public float speed = 0.75f;
    PlayerController player;
    public bool start = true;

    void Start()
    {
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (start)
        {
            start = false;
            transform.position = player.transform.position + new Vector3(0, 10, -10);
        }
        transform.LookAt(player.transform);
        transform.position = transform.position + transform.right * Time.deltaTime;
        //transform.RotateAround(player.transform.position, Vector3.forward, 20 * Time.deltaTime);
        
        //transform.Rotate(new Vector3(0, 1, 0) * speed);
    }
}
