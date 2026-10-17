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
