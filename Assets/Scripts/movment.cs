using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{

    private Vector2 moveInput;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float Speed = 1.0f;
    public float JumpForce = 1.0f;

    public GameObject cam3;
    public GameObject cam1;

    public PlayerController player;
    public bool primeraPersona;
    Ray ray;
    RaycastHit hit;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        cam1.SetActive(false);
        primeraPersona = false;
        player = transform.gameObject.GetComponentInChildren<PlayerController>();
    }

    void Update()
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
        if (primeraPersona)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
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

        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(hit.point.x, player.transform.position.y, hit.point.z) - player.transform.position), 5 * Time.deltaTime);
        }
       
    }

    private void FirstPersonMovement()
    {
        cam3.SetActive(false);
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
        xRotation = Mathf.Clamp(xRotation, -30, 30);

        player.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }

    private Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
