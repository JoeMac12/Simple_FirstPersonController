using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float Mousesensitivity = 2.0f;
    public Camera playerCamera;
    private Rigidbody rb;

    private float Forward;
    private float Sideways;
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
        Forward = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime; // Basic movement of the player for X and Z axis
        Sideways = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

        transform.Translate(Sideways, 0, Forward);

        rotateX = Input.GetAxis("Mouse X") * Mousesensitivity; // For rotating the camera
        rotateY -= Input.GetAxis("Mouse Y") * Mousesensitivity;
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
