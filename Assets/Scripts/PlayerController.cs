using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 2.0f;
    
    Animator anim;

    public Rigidbody rb;
    public Vector2 moveInput;

    public Vector3 jump;
    public bool isGrounded;

    public float shootSpeed, shootRate, speed, attack, baseShootSpeed, baseShootRate, baseSpeed, baseAttack;
    public int baseMaxHealth, maxHealth, health;
    public gun gun;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();   

    }

    private void Start()
    {
        jump = new Vector3(0.0f, 3.0f, 0.0f);
        anim = GetComponent<Animator>();

        baseSpeed = 5;
        speed = baseSpeed;
        baseAttack = 1;
        attack = baseAttack;
        baseShootRate = 1;
        shootRate = baseShootRate;
        baseShootSpeed = 10;
        shootSpeed = baseShootSpeed;
       
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        if (moveInput.y == 0 && moveInput.x == 0)
        {
            anim.SetBool("walk", false);
        }
        else{
            anim.SetBool("walk", true);
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
    private Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
