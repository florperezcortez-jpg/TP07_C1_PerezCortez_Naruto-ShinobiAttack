using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   [SerializeField] private int maxHealth = 3;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject deathEffectPrefab;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Bug: Si dos proyectiles entran en el mismo frame, Destroy no es inmediato y TakeDamage vuelve a llamar a Die():
    // suma dos veces scoreValue, hace dos TryDrop e instancia dos DeathEffect. Agregá un flag isDead y retorná si ya murió.
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioManager.Instance.PlayEnemyDeath();
        ScoreManager.Instance.AddScore(scoreValue);

        EnemyDrop drop = GetComponent<EnemyDrop>();

        if (drop != null)
        {
            drop.TryDrop();
        }
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
       Destroy(gameObject);
    }
}
