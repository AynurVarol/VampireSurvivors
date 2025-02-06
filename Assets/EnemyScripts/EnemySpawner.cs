using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyPrefabs; // Spawn edilecek d��man prefabs'leri (farkl� t�rler i�in bir array)
    public GameObject playerObject;
    public Transform[] spawnPoints; // D��manlar�n spawn olaca�� noktalar

    public float initialSpawnRate = 5.0f; // Ba�lang��ta d��man spawn etme h�z� (saniye cinsinden)
    public float spawnRateMultiplier = 1.05f; // Spawn h�z�n�n art�� oran� (bu de�er azald�k�a daha h�zl� spawn olacak)
    
    public int spawnAmount = 1; // Ba�lang��ta spawn edilen d��man say�s�
    public int maxSpawnAmount = 10; // En fazla spawn edilecek d��man say�s�
                                    // public Transform player;
   
    private float nextSpawnTime = 0f;
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate; // Ba�lang�� spawn h�z�n� ayarla
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies(); // D��manlar� spawn et
            nextSpawnTime = Time.time + currentSpawnRate; // Bir sonraki spawn zaman� ayarla
            currentSpawnRate *= spawnRateMultiplier; // Spawn h�z�n� art�r (daha h�zl� d��man spawn olur)

            // Spawn edilen d��man say�s�n� art�r (maksimum bir limite kadar)
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
            // Rastgele bir spawn noktas� se�
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Rastgele bir d��man t�r� se�
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // D��man� sahnede olu�tur ve referans�n� al
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // D��man objesine EnemyAI scriptini al ve player referans�n� ata
            EnemyAI enemyAI = spawnedEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.playerObject = playerObject;
            }
        }
    }
}