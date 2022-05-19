using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCamera : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] Transform player;
    [SerializeField] float speed = 5.0f;
    [SerializeField] MoveWalls wall1;
    private GameObject walls;

    private Vector3 target;
    private bool moure, cameraOnPlayer;
    public bool completedRoom, finished;
    private float step;

    private void Awake()
    {
        
    }

    private void Start()
    {
        step = speed * Time.deltaTime;
        moure = false;
        cameraOnPlayer = true;
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (wall1 != null)
        {
            walls = wall1.gameObject;
            walls.SetActive(false);
        }

        completedRoom = false;

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

        if (completedRoom && !finished)
        {
            StartCoroutine(roomFinished());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (walls != null)
            {
                walls.SetActive(true);
                wall1.ascend = true;
            }
            other.gameObject.GetComponent<PlayerController>().playerInBox = true;
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
            if (wall1 != null)
            {
                wall1.ascend = false;
            }
            
            other.gameObject.GetComponent<PlayerController>().playerInBox = false;
            moure = false;
            Debug.Log("Exiting");
        }
    }

    IEnumerator roomFinished()
    {
        finished = true;
        wall1.ascend = false;
        yield return new WaitForSeconds(2);
        restoreCamera();
        Destroy(walls);
    }

    public void restoreCamera()
    {
        player.gameObject.GetComponent<PlayerController>().playerInBox = false;
    }
}
