using UnityEngine;

public enum PickableType
{
    Life,
    Ramen,
    Chakra
}
public class Pickable : MonoBehaviour
{
    [SerializeField] private PickableType type;
    [SerializeField] private int amount = 1;

    [SerializeField] private float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player == null)
            return;

        switch (type)
        {
            case PickableType.Life:
                player.Heal(amount);
                AudioManager.Instance.PlayPickupLife();
                break;

            case PickableType.Ramen:
                ScoreManager.Instance.AddRamen();
                AudioManager.Instance.PlayPickupRamen();
                break;

            case PickableType.Chakra:
                player.GetComponent<PlayerPowerUp>().Activate();
                AudioManager.Instance.PlayChakra();
                break;
        }
        Destroy(gameObject);
    }
}
