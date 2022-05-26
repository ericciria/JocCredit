using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour

{

    GameObject pistola;

    public AudioSource so;
    public Image bloodEfect;
    [SerializeField] float jumpForce = 2.0f;
    
    public Animator anim;

    public float m_Thrust = 2000f;

    public Rigidbody rb;
    public Vector2 moveInput;

    public Vector3 jump;
    public bool isGrounded;
    public bool playerInBox;
    public bool cameraMort;

    public float shootSpeed, shootRate, speed, baseShootSpeed, baseShootRate, baseSpeed;
    public int baseMaxHealth, maxHealth, health, baseAttack, attack, jumpsLeft;
    public gun gun;
    private float r;
    private float g;
    private float b;
    private float a;

    public GameObject camGameOver;

    public GameObject mira;
    public GameObject cam3;
    public GameObject cam1;

    public bool isDead;

    public Image sliderhealth;

    private bool canCheck;
    public Vector3 lastPosition;

    public GameObject gameOver;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<gun>();
        cam1 = GameObject.Find("/Player/Body/primeraPerson");
        cam3 = GameObject.Find("/MainCamera");
        camGameOver = GameObject.Find("/cameraGameOver");
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
        baseMaxHealth = 2;
        maxHealth = baseMaxHealth;
        health = maxHealth;

        playerInBox = false;
        canCheck = true;
        cameraMort = true;
        mira = GameObject.Find("Player/Sang");
        gameOver = GameObject.Find("Player/gameOver");
        pistola = GameObject.Find("Player/Body/pistola");
        gameOver.SetActive(false);

    }

    void Update()
    {
        so = GetComponent<AudioSource>();
        a = Mathf.Clamp(a, 0, 1f);
        a -= 0.15f*Time.deltaTime;
        changeColor();
       

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (isGrounded && canCheck)
        {
            StartCoroutine(CheckPosition());
        }
        if (cameraMort)
        {
            cameraMort = false;
            camGameOver.SetActive(false);
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
    void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.isTrigger && !collision.collider.tag.Equals("Mur"))
        {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                isGrounded = true;
                jumpsLeft = 1;
            }
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    private Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void activarSang(int vida)
    {
        health -= vida;
        if (health <= 0)
        {
            Death();
        }
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

    IEnumerator CheckPosition()
    {
        canCheck = false;
        lastPosition = transform.position;
        yield return new WaitForSeconds(2);
        canCheck = true;
    }

    private void Jump()
    {
        if (jumpsLeft>0)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            jumpsLeft--;
        }
    }

    public void Death()
    {
        isDead = true;
        camGameOver.SetActive(true);
        camGameOver.GetComponent<cameraPlay>().start = true;
        cam3.SetActive(false);
        cam1.SetActive(false);
        anim.Play("dead", -1, 0f);
        gameOver.SetActive(true);
        pistola.SetActive(false);
        mira.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }


}
