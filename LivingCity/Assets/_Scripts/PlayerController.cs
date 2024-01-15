using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpCooldown = 1.0f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    public float slideAmount = 0.5f; // Controls the sliding effect when stopping

    private Rigidbody rb;
    private bool isGrounded;
    private float nextJumpTime;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        nextJumpTime = Time.time;
        cameraTransform = Camera.main.transform; // Assuming Cinemachine is controlling the main camera
        ToggleCursorState(true);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Time.time > nextJumpTime && isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            nextJumpTime = Time.time + jumpCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorState(!Cursor.lockState.Equals(CursorLockMode.Locked));
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction relative to the camera's orientation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0; // Remove camera pitch influence
        cameraRight.y = 0; // Remove camera roll influence
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;
        Vector3 moveVelocity = moveDirection * speed;

        // Apply sliding effect if no input is detected and the player is grounded
        if (horizontal == 0 && vertical == 0 && isGrounded)
        {
            moveVelocity.x *= slideAmount;
            moveVelocity.z *= slideAmount;
        }

        // Update the rigidbody's velocity
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            float gravityMultiplier = rb.velocity.y < 0 ? fallMultiplier : lowJumpMultiplier;
            rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    void ToggleCursorState(bool shouldLockCursor)
    {
        Cursor.lockState = shouldLockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !shouldLockCursor;
    }
}