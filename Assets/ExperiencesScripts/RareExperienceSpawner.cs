using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareExperienceSpawner : MonoBehaviour
{
    public GameObject rareExperiencePrefab; // nadir deneyim objesi
    public float spawnInterval = 10f; //spawn aralýðý


    public Vector3 spawnAreaMin; //spawn alanýnýn min köþesi
    public Vector3 spawnAreaMax; // max köþesi

    private void Start()
    {
        InvokeRepeating(nameof(SpawnExperienceObject), spawnInterval, spawnInterval);
    }

    void SpawnExperienceObject()
    {
        // rastgele konum belirler

        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
             Random.Range(spawnAreaMin.y, spawnAreaMax.y),
              Random.Range(spawnAreaMin.z, spawnAreaMax.z)
              );

        Instantiate(rareExperiencePrefab, spawnPosition, Quaternion.identity);



    }
}
