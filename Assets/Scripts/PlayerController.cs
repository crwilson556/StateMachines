using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // movement speed of the player
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  // get input from horizontal axis (left/right arrow keys)
        float verticalInput = Input.GetAxis("Vertical");  // get input from vertical axis (up/down arrow keys)

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);  // create a movement vector based on input
        transform.position += movement * moveSpeed * Time.deltaTime;  // move the player based on the movement vector and speed
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.SimpleMove(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
