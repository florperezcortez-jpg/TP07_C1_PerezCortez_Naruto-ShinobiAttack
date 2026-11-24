
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HeartDisplay hearts;




    private int currentHealth;
    private float nextDamageTime;
    private Animator animator;
    private Rigidbody2D rb;
    private bool chakraInvulnerable;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

   private void Start()
    {
        hearts.SetHealth(currentHealth, maxHealth);
    }
    // Bug: No hay flag de muerto. Si el jugador sigue en contacto con un enemigo, pasado invulnerabilityTime (1 s) TakeDamage vuelve a llamar a Die(),
    // que dispara otra vez el trigger Death y otro Invoke de GameOver (gameOverDelay es 1.5 s). Heal() también puede recargar corazones estando muerto.
    // Agregá private bool isDead y salí al principio de TakeDamage y Heal si es true.
    public void TakeDamage(int amount)
    {
        if (chakraInvulnerable)
            return;

        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + invulnerabilityTime;
        currentHealth -= amount;

        hearts.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            AudioManager.Instance.PlayPlayerHurt();
        }
    }

    public void SetChakraInvulnerable(bool value)
    {
        chakraInvulnerable = value;
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        hearts.SetHealth(currentHealth, maxHealth);
    } 
    private void Die()
    {
       AudioManager.Instance.StopMusic();
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        // Suggestion: Cacheá PlayerController y PlayerCombat en Awake como hacés con animator y rb, en vez de GetComponent acá.
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        gameManager.GameOver();
    }
}
