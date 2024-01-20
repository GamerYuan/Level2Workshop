using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpHeight;
    
    private CharacterController controller;
    private Vector3 velocity, move;
    private float gravity = -9.81f;
    private bool grounded;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // isGrounded does not work if Min Move Distance != 0
        grounded = controller.isGrounded;

        ApplyMove();
        ApplyRotation();
        ApplyGravity();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
    }
    
    void ApplyMove()
    {
        // Horizontal = A, D, LeftArrow, RightArrow
        // Vertical = W, S, UpArrow, DownArrow
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        controller.Move(move * Time.deltaTime * moveSpeed);
    }

    void ApplyRotation()
    {
        // Changes the forward direction of the object (Character facing where it's moving)
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    void ApplyGravity()
    {
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y += jumpHeight;
    }
}
