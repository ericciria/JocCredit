using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_movementVelocity = 10.0f;
    [SerializeField] float m_acceleration = 2.0f;
    [SerializeField] float m_rotationVelocity = 5.0f;
    [SerializeField] float jumpForce = 2.0f;
    Animator anim;

    Ray ray;
    RaycastHit hit;

    private Rigidbody rb;
    private Vector2 moveInput;

    public Vector3 jump;
    public bool isGrounded;

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

        /*Vector3 desiredVelocity = moveDirection * m_movementVelocity;
        Vector3 xAxis = Vector3.ProjectOnPlane(Vector3.right, Vector3.up);
        Vector3 yAxis = Vector3.ProjectOnPlane(Vector3.forward, Vector3.up);
        float currentXAxis = Vector3.Dot(rb.velocity, xAxis);
        float currentYAxis = Vector3.Dot(rb.velocity, yAxis);

        float maxSpeedChange = m_acceleration * Time.deltaTime;

        float newXVelocity = Mathf.MoveTowards(currentXAxis, desiredVelocity.x, m_acceleration);
        float newYVelocity = Mathf.MoveTowards(currentYAxis, desiredVelocity.y, m_acceleration);

        rb.velocity = new Vector3(newXVelocity, 0.0f, newYVelocity);*/


    }
    private void handleRotation()
    {
        transform.position = new Vector3(transform.position.x + moveInput.x, transform.position.y + moveInput.y, transform.position.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.point - transform.position), 5 * Time.deltaTime);
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
