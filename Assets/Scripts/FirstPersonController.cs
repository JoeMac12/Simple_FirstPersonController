using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Camera playerCamera; // Camera object

    public float walkSpeed = 5f; // Values for player movement
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2f;
    public float jumpPower = 8f;

    private float standHeight; // To store the original height
    public float crouchHeight = 1f; // Height of character controller while crouching

    public float gravity = 15f;

    public float mouseSensitivity = 2f; // Values for camera movement
    public float cameraLimit = 75f; // Limits how high or low you can look

    float rotationX = 0; // For camera rotation

    private bool isCrouching = false; // Used to check if the player is crouching

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        standHeight = characterController.height; // Store stand height for crouching height change

        Cursor.lockState = CursorLockMode.Locked; // Lock cursor and hide it
        Cursor.visible = false;
    }

    void Update()
    {
        float speed = (isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed)); // Toggle sprinting when holding left shift

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float moveX = speed * Input.GetAxis("Vertical");
        float moveY = speed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * moveX) + (right * moveY);

        if (Input.GetButton("Jump") && characterController.isGrounded) // Make the player jump
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY; // Normal y movement
        }

        if (!characterController.isGrounded) // Used to check if the player is on the ground
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C)) // Toggle crouching
        {
            isCrouching = !isCrouching;
            characterController.height = isCrouching ? crouchHeight : standHeight;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * mouseSensitivity; // Allow the player to look around
        rotationX = Mathf.Clamp(rotationX, -cameraLimit, cameraLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }
}
