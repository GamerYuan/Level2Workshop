using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpHeight;

    private CharacterController controller;
    private Vector3 velocity, move;
    private float gravity = -9.81f;
    private bool grounded;

    private Vector2 input;

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
    }

    void ApplyMove()
    {
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

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        move = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!grounded) return;

        velocity.y += jumpHeight;
    }
}
