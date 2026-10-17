using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private float spawnY = 0f;
    [SerializeField] private int maxEnemies = 3;

    private Transform player;
    private float nextSpawnTime;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (Time.time >= nextSpawnTime && enemiesParent.childCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;

        }
    }
    private void SpawnEnemy()
    {
        float side = Random.value < 0.5f ? -1f : 1f;
        Vector3 position = new Vector3(player.position.x + side * spawnDistance, spawnY, 0f);
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity, enemiesParent);
        enemy.GetComponent<EnemyMovement>().Alert();
    }
}