using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
   [SerializeField] private float speed = 5f;

    [SerializeField] private InputActionReference Move;

    private void Awake()
    {
   rb = GetComponent<Rigidbody2D>();
       
    }
    private void OnEnable()
    {
        Move.action.Enable();
    }

    private void OnDisable()
    {
        Move.action.Disable();
    }
    private void Update()
    {
        moveInput = Move.action.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        Movement();
    }

   private void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
}
