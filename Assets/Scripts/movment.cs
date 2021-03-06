using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movment : MonoBehaviour
{
    public int vida;
    private Vector2 moveInput;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float Speed = 1.0f;
    public float JumpForce = 1.0f;

    public GameObject cam3;
    public GameObject cam1;
    public GameObject cap;

    public PlayerController player;
    public bool primeraPersona;
    Ray ray;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    public bool isPaused, destroy;

    Transform spawnPoint;

    private void Awake()
    {
        if (FindObjectsOfType<movment>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    
    void Start()
    {
        //tuto = GetComponent<AudioSource>();
        //tuto.Play();
        cam1.SetActive(false);
        cam3 = GameObject.Find("MainCamera");
        primeraPersona = true;
        player = transform.gameObject.GetComponentInChildren<PlayerController>();
        cap = GameObject.Find("/Player/Body/Cube");
        isPaused = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam3 = GameObject.Find("/MainCamera");
        player.cam3 = cam3;
        spawnPoint = GameObject.Find("/SpawnPoint").GetComponent<Transform>();
        player.lastPosition = spawnPoint.position;
        player.transform.position = spawnPoint.position;
        if (scene.name.Equals("play"))
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
            player.mira.SetActive(true);
        }
        if (scene.name.Equals("gamewin"))
        {
            Destroy(gameObject);
        }


    }

    void Update()
    {
        if (!player.isDead)
        {

            if (player.speed < 5)
            {
                player.speed = 5;
            }
            else if (player.speed > 10)
            {
                player.speed = 10;
            }
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");



            if (Input.GetKeyDown(KeyCode.U))
            {
                primeraPersona = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                primeraPersona = false;

            }
            if (!isPaused)
            {
                if (primeraPersona)
                {
                    FirstPersonMovement();
                    cap.SetActive(false);
                }
                else
                {
                    ThirdPersonMovement();
                    cap.SetActive(true);
                }
            }
        }
    }

    private void ThirdPersonMovement()
    {
        cam3.SetActive(true);
        cam1.SetActive(false);

        
        transform.position = new Vector3(transform.position.x + moveInput.x * Time.deltaTime * player.speed, transform.position.y, transform.position.z + moveInput.y * Time.deltaTime * player.speed);

        if (!player.playerInBox)
        {
            Vector3 target = player.transform.position + new Vector3(0, 10, -10);
            cam3.transform.position = Vector3.MoveTowards(cam3.transform.position, target, 15 * Time.deltaTime);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ray = GetCameraRay();

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Debug.DrawLine(ray.origin, pointToLook, Color.cyan);

            //player.transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(pointToLook.x, player.transform.position.y, pointToLook.z) - player.transform.position), 10 * Time.deltaTime);
        }

        /*if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(hit.point.x, player.transform.position.y, hit.point.z) - player.transform.position), 5 * Time.deltaTime);
        }*/
       
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void FirstPersonMovement()
    {
        if (cam3 != null)
        {
            cam3.SetActive(false);
        }
        cam1.SetActive(true);


        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position = transform.position + ((player.transform.right * horizontal + player.transform.forward * vertical) * Time.deltaTime * player.speed);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Player Rotation
        float mouseX = Input.GetAxis("Mouse X") * 5;
        float mouseY = Input.GetAxis("Mouse Y") * 5;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40, 50);

        player.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }

    private Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    


}
