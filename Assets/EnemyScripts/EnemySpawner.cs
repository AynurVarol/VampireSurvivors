using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyPrefabs; // Spawn edilecek düþman prefabs'leri (farklý türler için bir array)
    public GameObject playerObject;
    public Transform[] spawnPoints; // Düþmanlarýn spawn olacaðý noktalar

    public float initialSpawnRate = 5.0f; // Baþlangýçta düþman spawn etme hýzý (saniye cinsinden)
    public float spawnRateMultiplier = 1.05f; // Spawn hýzýnýn artýþ oraný (bu deðer azaldýkça daha hýzlý spawn olacak)
    
    public int spawnAmount = 1; // Baþlangýçta spawn edilen düþman sayýsý
    public int maxSpawnAmount = 10; // En fazla spawn edilecek düþman sayýsý
                                    // public Transform player;
   
    private float nextSpawnTime = 0f;
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate; // Baþlangýç spawn hýzýný ayarla
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies(); // Düþmanlarý spawn et
            nextSpawnTime = Time.time + currentSpawnRate; // Bir sonraki spawn zamaný ayarla
            currentSpawnRate *= spawnRateMultiplier; // Spawn hýzýný artýr (daha hýzlý düþman spawn olur)

            // Spawn edilen düþman sayýsýný artýr (maksimum bir limite kadar)
            if (spawnAmount < maxSpawnAmount && Time.time % 60 == 0)
            {
                spawnAmount++;
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            // Rastgele bir spawn noktasý seç
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Rastgele bir düþman türü seç
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Düþmaný sahnede oluþtur ve referansýný al
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Düþman objesine EnemyAI scriptini al ve player referansýný ata
            EnemyAI enemyAI = spawnedEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.playerObject = playerObject;
            }
        }
    }
}