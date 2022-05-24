using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour

{
    public AudioSource so;
    public Image bloodEfect;
    [SerializeField] float jumpForce = 2.0f;
    
    Animator anim;

    public float m_Thrust = 2000f;

    public Rigidbody rb;
    public Vector2 moveInput;

    public Vector3 jump;
    public bool isGrounded;
    public bool playerInBox;

    public float shootSpeed, shootRate, speed, baseShootSpeed, baseShootRate, baseSpeed;
    public int baseMaxHealth, maxHealth, health, baseAttack, attack;
    public gun gun;
    private float r;
    private float g;
    private float b;
    private float a;

    public Image sliderhealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<gun>();

    }

    private void Start()
    {
        health = maxHealth;
        sliderhealth.fillAmount = 1;
        r = bloodEfect.color.r;
        g = bloodEfect.color.g;
        b = bloodEfect.color.b;
        a = bloodEfect.color.a;

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
        baseMaxHealth = 50;
        maxHealth = baseMaxHealth;
        health = maxHealth;

        playerInBox = false;
    }

    void Update()
    {
        so = GetComponent<AudioSource>();
        a = Mathf.Clamp(a, 0, 1f);
        a -= 0.15f*Time.deltaTime;
        changeColor();


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

    public void activarSang(int vida)
    {
        health -= vida;
        a += 0.30f;
        so.Play();
        changeColor();
        sliderhealth.fillAmount = (float)health / maxHealth;
    }

    private void changeColor()
    {
        Color c = new Color(r, g, b, a);
        bloodEfect.color = c;

    }
    
}
