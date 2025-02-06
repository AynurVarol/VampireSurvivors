using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSpawn : MonoBehaviour
{
   
    public GameObject[] characterPrefabs; // Karakter prefablarý
    public Transform spawnPoint;         // Spawn noktasý
    private GameObject playerInstance;   // Spawn edilen oyuncu objesi

    public delegate void PlayerSpawned(GameObject player);
   // public static event PlayerSpawned OnPlayerSpawned;
    public static event Action<GameObject> OnPlayerSpawned;


    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) ;
        {
            OnPlayerSpawned?.Invoke(player);
        }
    }
    public void SpawnPlayer(int characterIndex)
    {


        if (characterPrefabs != null && characterIndex >= 0 && characterIndex < characterPrefabs.Length)
        {
            GameObject playerInstance = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, spawnPoint.rotation);

            // Player spawn edildiðinde event'i tetikle
            OnPlayerSpawned?.Invoke(playerInstance);
        }



        else
        {
            Debug.LogError("Geçersiz karakter indeksi veya prefab listesi boþ!");
        }

    }

}
