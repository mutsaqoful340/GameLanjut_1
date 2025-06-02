using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Durasi = 30f;

    public InputPlayModule InputModule;
    public CharacterController Body;
    public Animator Anim;
    public float Speed = 5f;
    public float gravity = -20f;
    public float jumpHeight = 1f;
    public Transform CameraTransform;
    public float lookSpeedX = 2f;  // Mouse sensitivity for X axis
    public float lookSpeedY = 2f;  // Mouse sensitivity for Y axis

    private float verticalVelocity;
    private Vector3 moveDirection;
    private float rotationX = 0f;

    // State
    public bool IsIdle;
    public bool IsFall;
    public bool IsJump;

    // Singleton
    public static PlayerController Instance { get; private set; }

    private IEnumerator JumpCoroutine()
    {
        if (!IsJump && Body.isGrounded)
        {
            IsJump = true;
            verticalVelocity = Mathf.Sqrt(jumpHeight * -1 * gravity); // Physics-based jump velocity
            yield return null;
        }
    }

    private void Action(ActionState state)
    {
        switch (state)
        {
            case ActionState.Skill:
                StartCoroutine(JumpCoroutine());
                break;
        }
    }

    private void HandleGravityAndJump()
    {
        if (Body.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f; // Stick to ground
            }

            IsFall = false;

            // Apply jump force when grounded (whether moving or idle)
            if (IsJump)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -1f * gravity); // Physics-based jump velocity
                IsJump = false;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
            IsFall = true;
        }

        moveDirection.y = verticalVelocity;
    }

    private void Locomotion()
    {
        HandleGravityAndJump();

        Vector3 input = InputModule ? InputModule.MoveHandler.normalized : Vector3.zero;
        IsIdle = input == Vector3.zero;

        if (IsIdle)
        {
            Anim.SetFloat("Move", 0f);  // Correctly set idle animation
        }
        else
        {
            Anim.SetFloat("Move", 1f);  // Set movement animation
        }

        // Get camera-relative movement direction
        Vector3 camForward = Vector3.Scale(CameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = CameraTransform.right;
        Vector3 desiredDirection = camForward * input.z + camRight * input.x;
        desiredDirection.Normalize();

        // Rotate player to face movement direction
        if (desiredDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        moveDirection = desiredDirection * Speed;
        moveDirection.y = verticalVelocity;

        // Animate & move
        Body.Move(moveDirection * Time.deltaTime);
    }

    private void Update()
    {
        Locomotion();

        // Mouse look adjustments for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Invert mouse Y if necessary (fixes the inverted vertical mouse movement)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Clamp the vertical rotation to avoid flipping

        // Rotate camera around the X axis (up/down)
        CameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Invert the mouse X axis (fixes the inverted horizontal mouse movement)
        mouseX = -mouseX;  // Flip the horizontal input

        // Rotate player around the Y axis (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Check for jump input (replace 'JumpInput' with actual input from your InputPlayModule)
        if (InputModule != null && InputModule.JumpInput)  // Replace with your actual jump input logic
        {
            if (!IsJump && Body.isGrounded)  // Allow jump if grounded and not already jumping
            {
                StartCoroutine(JumpCoroutine());
            }
        }

        // Toggle mouse lock (you can replace "Escape" with any key you prefer)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMouseLock();
        }
    }


    private void ToggleMouseLock()
    {
        // Toggle mouse lock state
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;  // Unlock the mouse
            Cursor.visible = true;  // Make cursor visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;  // Lock the mouse
            Cursor.visible = false;  // Hide cursor
        }
    }

    private void Start()
    {
        Instance = this;
        Body = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();

        if (CameraTransform == null)
        {
            CameraTransform = Camera.main.transform;
        }

        if (InputModule != null)
        {
            InputModule.OnAction = Action;
        }

        // Lock the cursor at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
