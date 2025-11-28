using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRangeX = 8f;
    [SerializeField] private float spawnY = 6f;

    [SerializeField] private float difficultyIncreaseRate = 0.05f;
    [SerializeField] private float minSpawnRate = 0.5f;
    [SerializeField] private float difficultyTimer = 30f;

    private float spawnTimer = 0f;
    private float difficultyTimerCounter = 0f;
    private float currentSpawnRate;

    private void Start()
    {
        currentSpawnRate = spawnRate;

        // Prefab dizisi boþsa hata bas — Unity 6’da null check önemli
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawner: enemyPrefabs boþ, spawn yapamazsýn!");
        }
    }

    private void Update()
    {
        // GameManager null ise veya oyun kapalý ise hiçbir þey yapma
        if (GameManager.Instance == null || !GameManager.Instance.IsGameActive())
            return;

        // Timers
        spawnTimer += Time.deltaTime;
        difficultyTimerCounter += Time.deltaTime;

        // Zorluk artýþý
        if (difficultyTimerCounter >= difficultyTimer)
        {
            IncreaseDifficulty();
            difficultyTimerCounter = 0f;
        }

        // Spawn zamaný geldiyse
        if (spawnTimer >= currentSpawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
            return;

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Enemy seçme
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefab = enemyPrefabs[index];

        if (prefab != null)
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("EnemySpawner: enemyPrefabs içinde null bir prefab var.");
        }
    }

    private void IncreaseDifficulty()
    {
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
        // Debug.Log($"Yeni spawn rate: {currentSpawnRate}");
    }
}
