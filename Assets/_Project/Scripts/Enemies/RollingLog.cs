using UnityEngine;

public class RollingLog : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float rotationSpeed = 360f;

    private Rigidbody2D rb;
    private Transform player;
    private float direction = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;
    }
    public void SetDirection(float newDirection)
    {
        direction = Mathf.Sign(newDirection);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            direction * speed, 0f);

        rb.MoveRotation(rb.rotation - direction * rotationSpeed * Time.fixedDeltaTime);
        if (player != null && direction * (transform.position.x - player.position.x) > 0f)
        {
            Destroy(gameObject);
            return;
        }
        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
