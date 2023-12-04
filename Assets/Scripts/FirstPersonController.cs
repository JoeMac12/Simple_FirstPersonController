using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float jumpForce = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Camera playerCamera;
    private Rigidbody rb;

    private float forward;
    private float sideways;
    private float rotateX;
    private float rotateY;

    private bool isGrounded; // For ground checking

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift)) // Sprint if holding shift key
        {
            currentSpeed = sprintSpeed;
        }

        forward = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime; // Basic movement of the player for X and Z axis
        sideways = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;

        transform.Translate(sideways, 0, forward);

        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity; // For rotating the camera
        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotateY = Mathf.Clamp(rotateY, -60f, 60f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);
        transform.Rotate(0, rotateX, 0);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f); // Player jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
