using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRangeX = 8f;
    [SerializeField] private float spawnY = 6f;

    [SerializeField] private float difficultyIncreaseRate = 0.05f;
    [SerializeField] private float minSpawnRate = 0.5f;
    [SerializeField] private float difficultyTimer = 30f; // Her 30 saniyede zorluk artar

    private float nextSpawnTime = 0f;
    private float gameTime = 0f;
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = spawnRate;
    }

    void Update()
    {
        // Oyun devam ediyorsa spawn yap
        if (GameManager.Instance != null && GameManager.Instance.IsGameActive()) 
        {
            gameTime += Time.deltaTime;

            // Zorluk artýþý
            if (gameTime >= difficultyTimer)
            {
                IncreaseDifficulty();
                gameTime = 0f;
            }

            // Düþman spawn
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + currentSpawnRate;
            }
        }
    }

    void SpawnEnemy()
    {

        // Rastgele pozisyon
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Rastgele düþman seç
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomIndex];

        // Düþman oluþtur
        Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
    }

    void IncreaseDifficulty()
    {
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
        
    }
}
