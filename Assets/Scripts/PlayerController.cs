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
        //handleRotation();
    }

    private void handleMovement()
    {
        Vector3 moveDirection = transform.forward * moveInput.y;
        moveDirection += transform.right * moveInput.x;
        moveDirection.y = 0.0f;
        moveDirection.Normalize();

        //movement
        Vector3 moveVector = moveDirection * m_movementVelocity;
        moveVector.y = rb.velocity.y; // aplica la gravetat
        rb.velocity = moveVector;


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
        Vector3 moveDirection = transform.forward * moveInput.y;
        moveDirection += transform.right * moveInput.x;
        moveDirection.y = 0.0f;
        moveDirection.Normalize();

        //Rotation
        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }
        Quaternion desiredPosition = Quaternion.LookRotation(moveDirection);
        Quaternion finalRotation = Quaternion.Slerp(transform.rotation, desiredPosition, m_rotationVelocity * Time.deltaTime);
        transform.rotation = finalRotation;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
}
