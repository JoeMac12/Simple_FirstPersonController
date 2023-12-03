using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float Mousesensitivity = 2.0f;
    public Camera playerCamera;

    private float Forward;
    private float Sideways;
    private float rotateX;
    private float rotateY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the curser
    }

    void Update()
    {
        Forward = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime; // Basic movement of the player for X and Z axis
        Sideways = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

        transform.Translate(Sideways, 0, Forward);

        rotateX = Input.GetAxis("Mouse X") * Mousesensitivity; // For rotating the camera
        rotateY -= Input.GetAxis("Mouse Y") * Mousesensitivity;
        rotateY = Mathf.Clamp(rotateY, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);
        transform.Rotate(0, rotateX, 0);
    }
}
