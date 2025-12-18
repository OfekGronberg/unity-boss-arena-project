using UnityEngine;

// This script controls basic player movement using a Rigidbody
public class PlayerMovement : MonoBehaviour
{
    // How fast the player moves forward/back/sideways
    public float moveSpeed = 6f;

    // How fast the player turns to face the movement direction (degrees per second)
    public float turnSpeed = 720f;

    // Reference to the Rigidbody attached to the Player
    Rigidbody rb;

    // Called once when the object is created
    void Awake()
    {
        // Get and store the Rigidbody so we can move it with physics
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called at a fixed time step and is used for physics movement
    void FixedUpdate()
    {
        // Read keyboard input:
        // Horizontal = A (-1) / D (+1)
        // Vertical   = S (-1) / W (+1)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Convert input into a direction vector on the XZ plane
        // normalized ensures diagonal movement is not faster
        Vector3 input = new Vector3(h, 0f, v).normalized;

        // If no input is pressed, stop here (prevents jitter)
        if (input.sqrMagnitude < 0.001f) return;

        // Get the camera's forward and right directions
        // This allows movement relative to where the camera is facing
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // Remove vertical influence so the player stays on the ground
        camForward.y = 0f;
        camRight.y = 0f;

        // Normalize after removing Y so vectors stay unit length
        camForward.Normalize();
        camRight.Normalize();

        // Combine input with camera direction:
        // W/S moves forward/back relative to camera
        // A/D moves left/right relative to camera
        Vector3 moveDir =
            (camForward * input.z + camRight * input.x).normalized;

        // Create a rotation that looks in the movement direction
        Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);

        // Smoothly rotate the player toward the target direction
        rb.MoveRotation(
            Quaternion.RotateTowards(
                rb.rotation,
                targetRot,
                turnSpeed * Time.fixedDeltaTime
            )
        );

        // Move the player in the movement direction using physics
        rb.MovePosition(
            rb.position + moveDir * moveSpeed * Time.fixedDeltaTime
        );
    }
}
