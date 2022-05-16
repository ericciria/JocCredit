using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
  
    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + moveInput.x * Time.deltaTime * 5, transform.position.y, transform.position.z + moveInput.y * Time.deltaTime * 5);

    }
}
