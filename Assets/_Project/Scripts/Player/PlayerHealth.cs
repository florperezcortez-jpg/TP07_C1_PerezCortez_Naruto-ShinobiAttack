
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private GameManager gameManager;

    private int currentHealth;
    private float nextDamageTime;
    private Animator animator;
    private Rigidbody2D rb;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

   
    public void TakeDamage(int amount)
    {
        if (Time.time < nextDamageTime)
            return;

        nextDamageTime = Time.time + invulnerabilityTime;
        currentHealth -= amount;

       

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            AudioManager.Instance.PlayPickupLife();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    } 
    private void Die()
    {
       AudioManager.Instance.StopMusic();
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        gameManager.GameOver();
    }
}
