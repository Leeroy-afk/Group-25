using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class FPController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float fallGravityMultiplier = 2f;
    [SerializeField] private float maxFallSpeed = -30f;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private float crouchTransitionSpeed = 10f;

    [Header("Look")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float verticalLookLimit = 90f;

    private CharacterController controller;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Vector3 velocity;
    private float verticalRotation;

    private bool isCrouching;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleGravity();
        HandleCrouch();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();
    }

    private void HandleMovement()
    {
        Vector3 inputDirection =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        float currentSpeed = moveSpeed;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }


        Vector3 targetVelocity = inputDirection * currentSpeed;

            float currentAcceleration = moveInput.magnitude > 0.1f
                ? acceleration
                : deceleration;

        velocity.x = Mathf.MoveTowards(
            velocity.x,
            targetVelocity.x,
            currentAcceleration * Time.deltaTime
        );

        velocity.z = Mathf.MoveTowards(
            velocity.z,
            targetVelocity.z,
            currentAcceleration * Time.deltaTime
        );

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if (velocity.y < 0f)
        {
            velocity.y += gravity * fallGravityMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(
            verticalRotation,
            -verticalLookLimit,
            verticalLookLimit
        );

        cameraTransform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleCrouch()
    {
        float targetHeight = isCrouching
            ? crouchingHeight
            : standingHeight;

        controller.height = Mathf.Lerp(
            controller.height,
            targetHeight,
            crouchTransitionSpeed * Time.deltaTime
            );
    }
}