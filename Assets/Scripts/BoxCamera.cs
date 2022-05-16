using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCamera : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] Transform player;
    [SerializeField] float speed = 5.0f;

    private Vector3 target;
    private bool moure, cameraOnPlayer;
    private float step; 

    private void Start()
    {
        step = speed * Time.deltaTime;
        moure = false;
        cameraOnPlayer = true;

    }

    private void Update()
    {
        if (moure)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, target, step);
        }
        else if (!cameraOnPlayer)
        {
            target = player.position + new Vector3(0, 10, -10);
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, target, step);
            if (camera.transform.position == target)
            {
                cameraOnPlayer = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(camera.transform.position);
            Debug.Log(target);

            moure = true;
            Debug.Log("Entering");
            camera.gameObject.transform.parent = null;
            target = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z - 15);

            cameraOnPlayer = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moure = false;
            Debug.Log("Exiting");
            camera.transform.SetParent(player.parent);

            Debug.Log(camera.transform.position);
            Debug.Log(target);
        }
    }
}
