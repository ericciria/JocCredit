using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{

    private Vector2 moveInput;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float Speed = 1.0f;
    public float JumpForce = 1.0f;

    public GameObject desactivarCam3;
    public GameObject desactivarCam1;

    public PlayerController player;
    public bool primeraPersona;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Start()
    {
        desactivarCam1.SetActive(false);
        primeraPersona = false;
        player = transform.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (movementSpeed < 2)
        {
            movementSpeed = 2;
        }
        else if (movementSpeed > 10)
        {
            movementSpeed = 10;
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
        desactivarCam3.SetActive(true);
        desactivarCam1.SetActive(false);

        
        transform.position = new Vector3(transform.position.x + moveInput.x * Time.deltaTime * 5, transform.position.y, transform.position.z + moveInput.y * Time.deltaTime * 5);
    }

    private void FirstPersonMovement()
    {
        desactivarCam3.SetActive(false);
        desactivarCam1.SetActive(true);

        Vector3 moveDirection = transform.forward * moveInput.y;
        moveDirection += transform.right * moveInput.x;
        moveDirection.y = 0.0f;
        moveDirection.Normalize();

        //movement
        //Vector3 moveVector = moveDirection * player.speed;
        moveVector.y = rb.velocity.y; // aplica la gravetat
        rb.velocity = moveVector;

    }
}
