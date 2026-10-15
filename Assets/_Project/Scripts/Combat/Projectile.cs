
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public void SetDirection(float direction)
    {
        if (rb == null)
            return;

        rb.linearVelocity = new Vector2(direction * speed, 0f);
        if (sr != null)
        {
            sr.flipX = direction < 0f;
        }
        if (direction < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 30f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -30f);
        }
    }
}