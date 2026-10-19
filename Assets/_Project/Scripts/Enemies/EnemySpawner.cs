
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesParent;

    [SerializeField] private GameObject rollingLogPrefab;
    [SerializeField] private Transform rollingLogsParent;

    [SerializeField] private float spawnInterval = 3.5f;
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float difficultyStepSeconds = 30f;
    [SerializeField] private float intervalReduction = 0.5f;
    [SerializeField] private float spawnY = -6.5f;

    [SerializeField] private int maxEnemies = 3;

    private Transform player;
    private float nextSpawnTime;

    private float gameStartTime;

    private void Start()
    {
        gameStartTime = Time.time;

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

        if (Time.time < nextSpawnTime)
            return;

        if (enemiesParent != null && enemiesParent.childCount > 0)
        {
            if (enemiesParent.childCount < maxEnemies)
                SpawnEnemy();

           nextSpawnTime = Time.time + GetCurrentSpawnInterval();
            return;
        }
        if (rollingLogsParent != null && rollingLogsParent.childCount > 0)
        {
            nextSpawnTime = Time.time + GetCurrentSpawnInterval();
            return;
        }
       
        bool spawnLog = rollingLogPrefab != null && Random.value < 0.5f;

        if (spawnLog)
            SpawnRollingLog();
        else if (enemyPrefab != null)
            SpawnEnemy();

        nextSpawnTime = Time.time + GetCurrentSpawnInterval();

    }

    private float GetCurrentSpawnInterval()
    {
        float elapsedTime = Time.time - gameStartTime;

        int difficultyLevel = Mathf.FloorToInt(elapsedTime / difficultyStepSeconds);

        float interval = spawnInterval - difficultyLevel * intervalReduction;

        return Mathf.Max(minSpawnInterval, interval);
    }
    private Vector3 GetSpawnPosition(out float side)
    {
        side = Random.value < 0.5f ? -1f : 1f;

        float distance = spawnDistance;
       
        return new Vector3(player.position.x + side * spawnDistance,
            spawnY,
            0f);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || enemiesParent == null)
            return;

        Vector3 position = GetSpawnPosition(out _);

        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity, enemiesParent);

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
            movement.Alert();
    }

    private void SpawnRollingLog()
    {
        if (rollingLogPrefab == null || rollingLogsParent == null)
            return;

        Vector3 position = GetSpawnPosition(out float side);

        GameObject log = Instantiate(rollingLogPrefab, position, Quaternion.identity, rollingLogsParent);
        RollingLog rollingLog = log.GetComponent<RollingLog>();

        if (rollingLog != null)
        {
            rollingLog.SetDirection(-side);
        }
    }
}
