using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private float moveSpeed;
    private bool isRunning = false;
    private Vector2 moveInput;

    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        moveSpeed = walkSpeed;
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        bool isWalking = move.magnitude > 0.1f && !isRunning;

        // Animator sync
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
    }

    // Called by Input System → Movement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Called by Input System → Sprint (Shift key)
    public void OnSprint(InputValue value)
    {
        if (value.isPressed)
        {
            moveSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            moveSpeed = walkSpeed;
            isRunning = false;
        }
    }

    // Called by Input System → Jump (Space key)
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("Jump");
        }
    }
}
