using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
  
    private Vector2 moveInput;
    public int movementSpeed;

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
        transform.position = new Vector3(transform.position.x + moveInput.x * Time.deltaTime * movementSpeed, transform.position.y, transform.position.z + moveInput.y * Time.deltaTime * movementSpeed);

    }
}
