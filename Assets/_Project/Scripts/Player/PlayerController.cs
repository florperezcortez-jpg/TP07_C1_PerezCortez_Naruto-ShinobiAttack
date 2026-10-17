

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float moveInput;
    private bool isGrounded;
    public bool IsGrounded => isGrounded;
   [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private InputActionReference Move;
    [SerializeField] private InputActionReference Jump;

    private void Awake()
    {
   rb = GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
       
    }
    private void OnEnable()
    {
        Move.action.Enable();
        Jump.action.Enable();
    }

    private void OnDisable()
    {
        Move.action.Disable();
        Jump.action.Disable();
    }
    private void Update()
    {
        moveInput = Move.action.ReadValue<Vector2>().x;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
       
        if (Jump.action.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AudioManager.Instance.PlayJump();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Flip()
    {
        float distance = Mathf.Abs(firePoint.localPosition.x);
        if (moveInput > 0)
        {
            sr.flipX = false;
            firePoint.localPosition = new Vector3(distance, firePoint.localPosition.y, firePoint.localPosition.z);
        }
        if (moveInput < 0)
        {
            sr.flipX = true;
            firePoint.localPosition = new Vector3(-distance, firePoint.localPosition.y, firePoint.localPosition.z);
        }
    }
   private void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        

    }
}
