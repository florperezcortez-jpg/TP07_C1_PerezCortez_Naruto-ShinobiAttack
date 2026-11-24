using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Warning: OnCollisionStay2D corre en cada paso de física mientras dura el contacto, y acá se hace GetComponent cada vez.
        // Usá CompareTag o TryGetComponent, o resolvé el daño en OnCollisionEnter2D y dejá que la invulnerabilidad de PlayerHealth regule el resto.
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
    if (player != null )
        {
            player.TakeDamage(damage);
        }
    }
}
