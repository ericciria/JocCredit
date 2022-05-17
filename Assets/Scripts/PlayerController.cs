using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 2.0f;
    Animator anim;

    Ray ray;
    RaycastHit hit;

    private Rigidbody rb;
    private Vector2 moveInput;

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

        ray = GetCameraRay();
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            checkCameraRay(hit);
        }
    }

    private void checkCameraRay(RaycastHit hit)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position), 5 * Time.deltaTime);        
    }

    private void FixedUpdate()
    {
        handleMovement();
        //handleRotation();
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
